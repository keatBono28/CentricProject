using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentricProject.Models
{
    public class RecognitionModel
    {
        [Key]
        public int recognitionId { get; set; }
        public int recognizerId { get; set; }
        [Display(Name = "Who do you want to recognize?")]
        [Required(ErrorMessage = "You must select someone to recognize!")]
        public int recognizedId { get; set; }
        public virtual ProfileDetails Recognized { get; set; }
        [Display(Name = "Core Value")]
        [Required(ErrorMessage = "You must recognize this employee for a core value of Centric!")]
        [StringLength(100)]
        public string coreValue { get; set; }
        [Display(Name = "Leave a comment: ")]
        [StringLength(250)]
        public string comments { get; set; }
        public DateTime createDate { get; set; }
    }

}