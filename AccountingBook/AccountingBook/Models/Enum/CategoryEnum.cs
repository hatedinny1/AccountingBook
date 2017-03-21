using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountingBook.Models.Enum
{
    public enum CategoryEnum
    {
        /// <summary>
        /// { 0 : 支出 }
        /// </summary>
        [Display(Name = "支出")]
        Expenditure,
        /// <summary>
        /// { 1 : 收入 }
        /// </summary>
        [Display(Name = "收入")]
        Income,        
    }
}