using System.Collections.Generic;

namespace MimicAPI.Models.DTO
{
    public abstract class BaseDTO
    {
        public List<LinkDTO> links { get; set; }
    }
}
