using Core.Entities;
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
	[Table("Categories")]
	public class ECategories : IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		private string _UserId;
		[Required]
		[MaxLength(60)]
		public string UserId
		{
			get { return _UserId; }
			set { _UserId = value.SanitizeUrl(); }
		}

		private string _Name;
		[Required]
		[MaxLength(255)]
		[DataType(DataType.Text)]
		public string Name
		{
			get { return _Name; }
			set { _Name = value.Sanitize(); }
		}

		private string _Description;
		[MaxLength(255)]
		[DataType(DataType.Text)]
		public string Description
		{
			get { return _Description; }
			set { _Description = value.Sanitize(); }
		}

		[Required]
		public int DisplayOrder { get; set; }

		[Required]
		public bool Published { get; set; }

        public bool IsDefault { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }
	}
}
