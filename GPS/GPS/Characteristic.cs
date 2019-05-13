using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GPS
{
    class Characteristic
    {
        public int Id { get; set; }
        public int ElementId { get; set; }
        public Element Element { get; set; }
       /* 
        [Required]
        public virtual int CharacteristicTypeId
        {
            get
            {
                return (int)this.CharacteristicType;
            }
            set
            {
                CharacteristicType = (CharacteristicTypes)value;
            }
        }*/
        [EnumDataType(typeof(CharacteristicTypes))]
        public CharacteristicTypes CharacteristicType { get; set; }

        public enum CharacteristicTypes
        {
            Trgovina = 0,
            Benzinska = 1,
            Restoran = 2,
            Kafić = 3,
            Garaža = 4,
            Pošta = 5,
            Ljekarna = 6
        }



    }
}
