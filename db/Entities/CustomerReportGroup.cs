using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiveways.Insight.Model.Entities
{
    public class CustomerReportGroup
    {
        public int Id { get; set; }

        public int ReportGroupId { get; set; }

        [ForeignKey("ReportGroupId")]
        public ReportGroup ReportGroup { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

    }
}
