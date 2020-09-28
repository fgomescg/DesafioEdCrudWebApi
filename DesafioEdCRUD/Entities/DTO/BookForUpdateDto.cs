﻿using Entities.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class BookForUpdateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(40, ErrorMessage = "Name can't be longer than 40 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Company is required")]
        [StringLength(40, ErrorMessage = "Company cannot be loner then 4 characters")]
        public string Company { get; set; }

        [Required(ErrorMessage = "PublishYear is required")]
        [StringLength(4, ErrorMessage = "PublishYear cannot be loner then 4 characters")]
        public string PublishYear { get; set; }

        [DefaultValue(1)]
        public int Edit { get; set; }

        public decimal Value { get; set; }

        public IEnumerable<BookAuthor> BookAuthors { get; set; }
        public IEnumerable<BookSubject> BookSubjects { get; set; }
    }
}
