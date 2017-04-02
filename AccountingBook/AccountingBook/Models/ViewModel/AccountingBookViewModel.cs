using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingBook.Models.Enum;
using ValidateSample.Filters;

namespace AccountingBook.Models.ViewModel
{
    public class AccountingBookViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("類別")]
        [Required]
        public CategoryEnum? Category { get; set; }

        [DisplayName("日期")]      
        [Required]
        [DataType(DataType.DateTime)]
        [RemoteDoublePlus("NotAfterToday", "Validate", AreaReference.UseRoot,ErrorMessage = "日期不得於今日之後。")]
        public DateTime Date { get; set; }

        [DisplayName("金額")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:#,##0}")]
        [Required]        
        [Range(0, int.MaxValue, ErrorMessage = "{0} 請輸入正整數。")]
        public int Money { get; set; }

        [DisplayName("備註")]
        [DataType(DataType.MultilineText)]
        [Required]
        [StringLength(100, ErrorMessage = "{0} 至多輸入100字。")]
        public string Remark { get; set; }
    }
}