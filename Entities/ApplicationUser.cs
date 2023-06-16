using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Entities
{
	public class ApplicationUser : IdentityUser
	{
		private string _Name;
		[MaxLength(55)]
		public string Name
		{
			get { return _Name; }
			set { _Name = value.ToTitleCase(); }
		}

		private string _Surname;
		[MaxLength(55)]
		public string Surname
		{
			get { return _Surname; }
			set { _Surname = value.ToUpper(); }
		}

		[NotMapped]
		public virtual string FullName
		{
			get { return $"{Name} {Surname}"; }
		}

        public byte[] SecretKey { get; set; }

        private string _Avatar;
		public string Avatar
		{
			get { return _Avatar; }
			set { _Avatar = string.IsNullOrWhiteSpace(value) ? "data:image/jpeg;base64,/9j/2wCEAAcFBQsODQoPDA4OEQ8LDQoKCwsLCQ8ICgkQDRUQDhgJDQ0PEhsYERIlHQ0OHyIWGhwdHyAgDhQvMy8eMicwMioBBwgICwoLFQsLFR4VFRUeHh4eHiYeKh4eHh4eHh4eHh4eHioqKiolHh4eHh4eKh4eHioqHioeHioeHh4eKh4eHv/CABEIAGQAZAMBIgACEQEDEQH/xAAbAAEAAgMBAQAAAAAAAAAAAAAABQYBAgMEB//aAAgBAQAAAAD6gAAAAAAA235ACasu0ZU9QO16yVyBA91zELWAM3j0FQjQEra8xlT1AnbIPLTuIey6gi6kFinwYoOotEyBReD/xAAXAQEBAQEAAAAAAAAAAAAAAAAAAQMC/9oACAECEAAAANQACcTSkzaUnE76M4amcNL/AP/EABcBAQEBAQAAAAAAAAAAAAAAAAABAgP/2gAIAQMQAAAAwARRN3AnRgNXA1TnWqc7/8QAIhAAAQIGAwADAAAAAAAAAAAAAQIDAAQRIDAxECFBMlBR/9oACAEBAAEIAvrAIKTjZZ/QOHGQYIwIT2LZlOjgZ+QtmdDADCFVsfXU4WQrzh0K8IwMs+mxbYMKTS5pFTe+jq6VG8BFstrAvZ5//8QAGBEBAQEBAQAAAAAAAAAAAAAAASAQEUD/2gAIAQIBAQgA8C4WSuD2G2v/xAAZEQEBAAMBAAAAAAAAAAAAAAABIBARMED/2gAIAQMBAQgA8AcWQwmoLK//xAAbEAABBAMAAAAAAAAAAAAAAAAwAUBBURBg4f/aAAgBAQAJPwLVrLAeP4Z2C1z/AP/EACcQAAECBQMEAgMAAAAAAAAAAAERMQAgITBBUWFxEIGRoVDxscHh/9oACAEBAAE/IfjCFh4CwE4PhLatWYGsADdGSh/PMECg4sKA1MASd4obB+yU7AQQ6VgIAj62kZGo53sr1ZlXQOhwYmQxMED2F/QNd5RNexyIOSH73mSBhzOurNX8moFwLCRI0JEr/L9W6f/EACgQAQAAAwcDBAMAAAAAAAAAAAERMUEAICEwUWGREHGhscHR8FCB8f/aAAgBAQABPxD8ZNB2SwaIzVYyzJ4x3u1gAABQIHQhQUUIGwe9mAQVBGmRtQD2rYAAkABpClwxDVvQxH14yCIuo5EulCqxcD8mQiCaA6JjauxMqrg4CmBoqjxxk4IwU2H5P1aKBGdYYnR0OITOyjKFlECJMSDkAAcJv5NtrBcgAYkiw7mEmg0XouScZT0sF6JioNyv1pe7qeHH3L6RiNcLfwIGF37TTIfcavX/xAAUEQEAAAAAAAAAAAAAAAAAAABQ/9oACAECAQk/AEf/xAAUEQEAAAAAAAAAAAAAAAAAAABQ/9oACAEDAQk/AEf/2Q==" : value; }
		}

        public DateTime CreationDate { get; set; }
    }
}
