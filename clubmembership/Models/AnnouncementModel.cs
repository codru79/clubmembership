using System.ComponentModel.DataAnnotations;

namespace clubmembership.Models
{
    public class AnnouncementModel
    {
        public Guid Idannouncemment { get; set; }

        //decorators for datetime type fields
        [DisplayFormat(DataFormatString="{0:d}")]
        [DataType(DataType.Date)]

        public DateTime ValidFrom { get; set; }

        //decorators for datetime type fields 
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime ValidTo { get; set; }

        //decorator for Title field 
        [StringLength(50, ErrorMessage= "Maxim 50 caractere")]
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;

        //decorators for datetime type fields 
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime? EventDateTime { get; set; }
        public string? Tags { get; set; }
    }
}
