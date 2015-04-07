using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using IRL.Services.Contracts;
using IRL.Dtos;
using AutoMapper;
using IRL.Model.AdventureWorks;

namespace IRL.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly IEmployeeService employeeService;

        public ConsultationController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var dto = new ConsultationMainDto();

            dto.EmployeeMetadata = employeeService.EmployeeMetadata().Select(x => x.MemberName).ToList();

            return View(dto);
        }

        [HttpPost]
        public ActionResult ConsultationResultPartial(IList<string> metadataToShow, 
                                                        string filterString, 
                                                        IList<string> filterArgumentValues,
                                                        IList<string> filterArgumentDataTypes)
        {
            if (metadataToShow == null || metadataToShow.Count == 0)
            {
                return PartialView("ConsultationResultErrorPartial");
            }

            var filterArgumentDtos = new List<FilterArgumentDto>();
            if (filterArgumentDataTypes != null && filterArgumentValues != null)
            {
                if (filterArgumentDataTypes.Count != filterArgumentValues.Count)
                {
                    throw new ArgumentException("FilterArgument Types and Values must have the same amount of items");
                }

                for (var i = 0; i < filterArgumentValues.Count; i++)
                {
                    var filterArgumentDto = new FilterArgumentDto()
                    {
                        DataValue = filterArgumentValues[i],
                        DataType = filterArgumentDataTypes[i]
                    };
                    filterArgumentDtos.Add(filterArgumentDto);
                }
            }

            var dto = new ConsultationResultMainDto();

            dto.EmployeeList = employeeService.GetEmployeesByMetadataAndFilter(metadataToShow, 
                                                filterString,
                                                filterArgumentDtos.GenericMapper<IList<FilterArgumentDto>, IList<FilterArgument>>())
                                  .GenericMapper<IList<vEmployee>, IList<EmployeeDto>>();

            dto.EmployeeMetadata = metadataToShow;
            dto.FilterString = filterString;
            dto.FilterArgumentValues = filterArgumentValues;
            dto.FilterArgumentDataTypes = filterArgumentDataTypes;

            return PartialView(dto);
        }

        [HttpGet]
        public ActionResult MetadataAccessor()
        {
            var metadata = employeeService.EmployeeMetadata();
            return Json(metadata, JsonRequestBehavior.AllowGet);
        }
    }
}
