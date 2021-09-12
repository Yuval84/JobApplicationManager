using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Areas.Identity.Data;

namespace JobApplication.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        //[Required]
        public string UserId { get; set; }

        [DisplayName("Role")]
        [Required(ErrorMessage = "Please Add Role")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Please Add Company name")]
        public string Company { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please Add Date")]
        public DateTime Date { get; set; }

        public string Url { get; set; }

        public string Status { get; set; }

        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }

        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        [DisplayName("Interview Date")]
        public DateTime InterviewDate { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}
