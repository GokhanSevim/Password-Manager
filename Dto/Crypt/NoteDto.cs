using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Dto.Crypt
{
	public class NoteDto : IDto
	{
		public int Id { get; set; }

		private string _UserId;
		[MaxLength(60)]
		public string UserId
		{
			get { return _UserId; }
			set { _UserId = value.SanitizeUrl(); }
		}

		public int CategoryId { get; set; }

		private string _CategoryTemp;
		[MaxLength(255)]
		public string CategoryTemp
		{
			get { return _CategoryTemp; }
			set { _CategoryTemp = value.Sanitize(); }
		}

		private string _Name;
		[MaxLength(255)]
		[Required]
		public string Name
		{
			get { return _Name; }
			set { _Name = value.Sanitize(); }
		}

		private string _Content;
		[Required]
		[DataType(DataType.MultilineText)]
		public string Content
		{
			get { return _Content; }
			set { _Content = value.Sanitize(); }
		}

		public bool PasswordReprompt { get; set; }

		public DateTime CreationTime { get; set; }
	}
}
