using Microsoft.EntityFrameworkCore;
using ShopTARge22.Core.Domain;
using ShopTARge22.Core.DTO;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge22.ApplicationServices.Services
{
	public class KindergartensServices : IKindergartensServices
	{
		private readonly ShopTARge22Context _context;

		public KindergartensServices
			(
				ShopTARge22Context context
			)
		{
			_context = context;
		}

		public async Task<Kindergartens> Create(KindergartenDTO dto)
		{
			Kindergartens kindergarten = new Kindergartens();

			kindergarten.Id = Guid.NewGuid();
			kindergarten.GroupName = dto.GroupName;
			kindergarten.ChildrenCount = dto.ChildrenCount;
			kindergarten.KindergartenName = dto.KindergartenName;
			kindergarten.Teacher = dto.Teacher;
			kindergarten.CreatedAt = DateTime.Now;
			kindergarten.ModifiedAt = DateTime.Now;

			await _context.Kindergartens.AddAsync(kindergarten);
			await _context.SaveChangesAsync();

			return kindergarten;
		}

		public async Task<Kindergartens> DetailsAsync(Guid id)
		{
			var result = await _context.Kindergartens
				.FirstOrDefaultAsync(x => x.Id == id);

			return result;
		}

		public async Task<Kindergartens> Update(KindergartenDTO dto)
		{
			Kindergartens domain = new();

			domain.Id = dto.Id;
			domain.GroupName = dto.GroupName;
			domain.ChildrenCount = dto.ChildrenCount;
			domain.KindergartenName = dto.KindergartenName;
			domain.Teacher = dto.Teacher;
			domain.CreatedAt = dto.CreatedAt;
			domain.ModifiedAt = DateTime.Now;

			_context.Kindergartens.Update(domain);
			await _context.SaveChangesAsync();

			return domain;
		}

		public async Task<Kindergartens> Delete(Guid id)
		{
			var kindergartenId = await _context.Kindergartens
				.FirstOrDefaultAsync(x => x.Id == id);

			_context.Kindergartens.Remove(kindergartenId);
			await _context.SaveChangesAsync();

			return kindergartenId;
		}
	}
}
