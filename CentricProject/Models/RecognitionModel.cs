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
        public virtual ProfileDetails Recognizer { get; set; }
        [Display(Name = "Who do you want to recognize?")]
        [Required(ErrorMessage = "You must select someone to recognize!")]
        public int recognizedId { get; set; }
        [Display(Name = "Core Value")]
        [Required(ErrorMessage = "You must recognize this employee for a core value of Centric!")]
        public CoreValues coreValue { get; set; }
        [Display(Name = "Leave a comment: ")]
        [StringLength(250)]
        public string comments { get; set; }
        public DateTime createDate { get; set; }
    }

    public enum CoreValues
    {
        Balance,
        Good,
        Innovation,
        Stewardship,
        Excellence,
        Culture,
        Intergrity
    }

}