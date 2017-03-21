using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountingBook.Models
{
    public class CategoryType
    {
        public enum Category
        {
            /// <summary>
            /// 收入
            /// </summary>
            [Display(Name = "收入")]
            Income = 0,
            /// <summary>
            /// 支出
            /// </summary>
            [Display(Name = "支出")]
            Expenditure
        }
    }
}