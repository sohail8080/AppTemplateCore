using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplateCore.Models.DataAnnotations
{
    public class FileUploadDA
    {
        //Regular Expression for allowing Word Document and PDF files only
        //([a-zA-Z0-9\s_\\.\-:])+(.doc|.docx|.pdf)$
 
        //Regular Expression for allowing Image files only
        //([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$
 
        //Regular Expression for allowing Text files only
        //([a-zA-Z0-9\s_\\.\-:])+(.txt)$
 
        //Regular Expression for allowing Excel files only
        //([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$

        [Required(ErrorMessage = "Please select file.")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public Object PostedFile { get; set; }

        //public HttpPostedFile ImageFile { get; set; }

        //[DisplayName("evidence")]
        //[Required(ErrorMessage = "You must provide evidence")]
        //[RegularExpression(@"^abc123.jpg$", ErrorMessage = "Stuff and nonsense")]
        //public HttpPostedFileWrapper Evidence { get; set; }

    }
}
