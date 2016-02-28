using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Data.FullDomain.Infrastructure
{
    public class MercuritusFullDomainUnitOfWorkBase : UnitOfWorkBase<MercuriusEntities>, IMercuritusFullDomainUnitOfWork
    {
        public MercuritusFullDomainUnitOfWorkBase(IDatabaseFactory<MercuriusEntities> databaseFactory)
            : base(databaseFactory)
        {
        }

        public IList<Tuple<string, string>> CommitHandled()
        {
            var errors = new List<Tuple<string, string>>();

            try
            {
                Commit();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationError in e.EntityValidationErrors)
                {
                    foreach (var ve in validationError.ValidationErrors)
                    {
                        errors.Add(new Tuple<string, string>(ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }

            return errors;
        }
    }
}
