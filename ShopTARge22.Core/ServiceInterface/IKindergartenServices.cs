using ShopTARge22.Core.Domain;
using ShopTARge22.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge22.Core.ServiceInterface
{
    public interface IKindergartensServices
    {
        Task<Kindergartens> Create(KindergartenDTO dto);
        Task<Kindergartens> DetailsAsync(Guid id);
        Task<Kindergartens> Delete(Guid id);
        Task<Kindergartens> Update(KindergartenDTO dto);
    }
}
