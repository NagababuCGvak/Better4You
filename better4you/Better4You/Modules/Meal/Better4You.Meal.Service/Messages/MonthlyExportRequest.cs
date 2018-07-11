using System.Runtime.Serialization;
using Better4You.Meal.ViewModel;
using Tar.Service.Messages;

namespace Better4You.Meal.Service.Messages
{
    [DataContract]
    public class MonthlyExportRequest : Request
    {
        [DataMember]
        public OrderReportFilterView Filter { get; set; }
    }
}