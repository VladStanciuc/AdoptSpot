using AdoptSpot.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptSpot.Models
{
    [Table("Image")]
    public class ImageModel : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
