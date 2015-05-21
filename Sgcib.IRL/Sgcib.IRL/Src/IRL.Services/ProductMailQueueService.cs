using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Windsor;
using EasyNetQ;
using FluentEmail;
using Mendeo.Common.Core;
using Mendeo.Mercurius.Data.FullDomain.Infrastructure;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Service.Contract;
using Mendeo.Common.Core.Extentions;
using Microsoft.Practices.ServiceLocation;

namespace Mendeo.Mercurius.Service
{
    public class ProductMailQueueService : IProductMailQueueService
    {
        private readonly IMercuritusFullDomainRepository<Product> _productRepository;
        private readonly IMercuritusFullDomainRepository<MailerTemplate> _mailerTemplateRepository;
        private readonly IMercuritusFullDomainRepository<ProductMailQueue> _productMailQueueRepository;
        private readonly IMercuritusFullDomainRepository<User> _userRepository;
        private readonly IMercuritusFullDomainUnitOfWork _unitOfWork;
        private readonly ICultureService _cultureService;
        private readonly IBus _bus;
        private ISubscriptionResult _currentSubscription;

        public ProductMailQueueService(IMercuritusFullDomainRepository<Product> productRepository,
                                        IMercuritusFullDomainRepository<MailerTemplate> mailerTemplateRepository,
                                        IMercuritusFullDomainRepository<ProductMailQueue> productMailQueueRepository,
                                        IMercuritusFullDomainRepository<User> userRepository,
                                        IMercuritusFullDomainUnitOfWork unitOfWork,
                                        ICultureService cultureService,
                                        IBus bus)
        {
            _productRepository = productRepository;
            _mailerTemplateRepository = mailerTemplateRepository;
            _productMailQueueRepository = productMailQueueRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _cultureService = cultureService;
            _bus = bus;
        }

        public IList<ProductMailQueueDto> GetProductMailQueueCreated()
        {
            var pmq =
                _productMailQueueRepository.DbSet.Where(
                    x => x.QueueStatusID == (int)QueueStatusEnum.CREATED && x.Activate == true);

            return Mapper.Map<IList<ProductMailQueue>, IList<ProductMailQueueDto>>(pmq.ToList());
        }

        public void QueueProductEmail(MailerTemplateTypeEnum mailerType, int productId)
        {
            var now = DateTime.Now;

            var template = _mailerTemplateRepository.DbSet.First(x => x.MailerTypeID == (int)mailerType);

            var productQueue = new ProductMailQueue()
            {
                ProductID = productId,
                MailerTemplateID = template.MailerTemplateID,
                Message = "Initial Queue Created",
                Activate = true,
                CreatedOn = now,
                ModifiedOn = now,
                QueueStatusID = (int)QueueStatusEnum.CREATED
            };

            _productMailQueueRepository.Add(productQueue);
            _unitOfWork.Commit();

            _bus.Publish(Mapper.Map<ProductMailQueue, ProductMailQueueDto>(productQueue));
        }

        public void ProcessQueueProductEmail(string subscriberId)
        {
            _currentSubscription = _bus.SubscribeAsync<ProductMailQueueDto>(subscriberId, pmq =>
            {

                return Task.Factory.StartNew(() =>
                {
                    var _productMailQueueRepositoryLocal =
                        ServiceLocator.Current.GetInstance<IMercuritusFullDomainRepository<ProductMailQueue>>();

                    var _userRepositoryLocal =
                        ServiceLocator.Current.GetInstance<IMercuritusFullDomainRepository<User>>();

                    var _unitWorkLocal =
                        ServiceLocator.Current.GetInstance<IMercuritusFullDomainUnitOfWork>();

                    Check.Require(pmq.ProductMailQueueID > 0, "productMailQueueId must be positive");

                    var productMailQueueToProcess = _productMailQueueRepositoryLocal.DbSet.Where(
                            x => x.ProductMailQueueID == pmq.ProductMailQueueID && x.Activate == true).FirstOrDefault();

                    Check.Require(productMailQueueToProcess != null, "productMailQueueId must be valid");
                    Check.Require(productMailQueueToProcess.QueueStatusID == (int)QueueStatusEnum.CREATED, "productMailQueue must be in CREATED mode");

                    try
                    {
                        var now = DateTime.Now;
                        productMailQueueToProcess.Message = "Processing...";
                        productMailQueueToProcess.ModifiedOn = now;
                        productMailQueueToProcess.QueueStatusID = (int)QueueStatusEnum.PROCESSING;
                        _productMailQueueRepositoryLocal.Update(productMailQueueToProcess);
                        _unitWorkLocal.Commit();

                        var p = productMailQueueToProcess.Product;
                        Check.Require(p.UserID.HasValue, "product must be assigned to a user, for email adress");
                        var u = _userRepositoryLocal.GetById(p.UserID.Value);
                        var template = productMailQueueToProcess.MailerTemplate;

                        var m = new FluentEmail.Email("mendeo.noreply@mendeo.com", "Mendeo - No Reply")
                            .To(u.EMail)
                            .Subject(template.Subject)
                            .UsingTemplate(template.Body, p, template.IsHtml);

                        m.Send();

                        var nowSent = DateTime.Now;
                        productMailQueueToProcess.Message = "Sent...";
                        productMailQueueToProcess.ModifiedOn = nowSent;
                        productMailQueueToProcess.QueueStatusID = (int)QueueStatusEnum.SUCCESS;
                        _productMailQueueRepositoryLocal.Update(productMailQueueToProcess);
                    }
                    catch (Exception e)
                    {
                        var nowError = DateTime.Now;
                        productMailQueueToProcess.Message = e.Message;
                        productMailQueueToProcess.ModifiedOn = nowError;
                        productMailQueueToProcess.QueueStatusID = (int)QueueStatusEnum.ERROR;
                        _productMailQueueRepositoryLocal.Update(productMailQueueToProcess);
                    }
                    finally
                    {
                        _unitWorkLocal.Commit();
                    }
                });

            });
        }


        public void DisposeQueueProductMail()
        {
            _currentSubscription.Dispose();
        }
    }
}
