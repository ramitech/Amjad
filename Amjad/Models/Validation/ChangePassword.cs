using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Amjad.Models
{
    public class ChangePassword
    {

       
        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Labels), ErrorMessageResourceName = "Required")]
        [Display(Name = "كلمة المرور الحالية")]
        public string Password { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Labels), ErrorMessageResourceName = "Required")]
        [Display(Name = "كلمة المرور الجديدة")]
        public string PasswordNew { get; set; }
        
        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Labels), ErrorMessageResourceName = "Required")]
        [Compare("PasswordNew", ErrorMessage = "كلمة المرور غير متطابقة")]
        [Display(Name = "تأكيد كلمة المرور")]
        public string PasswordConfirm { get; set; }


    }
}