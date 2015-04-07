using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRL.Dtos
{
    public class ConsultationResultMainDto
    {
        public IList<EmployeeDto> EmployeeList { get; set; }

        public IList<string> EmployeeMetadata { get; set; }
        public string FilterString { get; set; }
        public IList<string> FilterArgumentValues { get; set; }
        public IList<string> FilterArgumentDataTypes { get; set; }
    }
}
