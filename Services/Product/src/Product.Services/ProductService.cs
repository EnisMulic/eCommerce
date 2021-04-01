using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Interfaces;
using Product.Database;
using Product.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Services
{
    public class ProductService :
        CrudService<ProductResponse, ProductSearchRequest, Domain.Product, ProductInsertRequest, ProductUpdateRequest>,
        IProductService
    {
        private readonly IImageUploadService _imageUploadService;
        public ProductService(IImageUploadService imageUploadService, ProductDbContext context, IMapper mapper) : base(context, mapper)
        {
            _imageUploadService = imageUploadService;
        }

        public override async Task<PagedResponse<ProductResponse>> GetAsync(ProductSearchRequest search = null)
        {
            var list = await _context.Set<Domain.Product>()
                .Include(i => i.Image)
                .ToListAsync();

            var response = _mapper.Map<List<ProductResponse>>(list);
            return new PagedResponse<ProductResponse>(response);
        }

        public override async Task<ProductResponse> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<Domain.Product>()
                .Include(i => i.Image)
                .Include(i => i.Categories)
                .Include(i => i.AttributeValues)
                .Include(i => i.OptionValues)
                .SingleOrDefaultAsync(i => i.Id == id);

            return _mapper.Map<ProductResponse>(entity);
        }

        public override async Task<ProductResponse> InsertAsync(ProductInsertRequest request)
        {
            var uri = await _imageUploadService.UploadAsync(request.Image);

            var image = new Image()
            {
                Uri = uri
            };
            await _context.Set<Image>().AddAsync(image);

            var entity = _mapper.Map<Domain.Product>(request);
            entity.Image = image;

            await _context.Set<Domain.Product>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(entity);
        }

        public override async Task<ProductResponse> UpdateAsync(Guid id, ProductUpdateRequest request)
        {
            var entity = await _context.Set<Domain.Product>()
                .Include(i => i.Image)
                .SingleOrDefaultAsync(i => i.Id == id);

            if (entity == null)
            {
                return default;
            }

            var image = await _context.Set<Image>().FindAsync(entity.ImageId);
            var uri = await _imageUploadService.UploadAsync(request.Image);

            image.Uri = uri;
            _context.Set<Image>().Update(image);

            _mapper.Map(request, entity);

            _context.Set<Domain.Product>().Update(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(entity);
        }

        public async Task<ProductResponse> AddAttributes(Guid id, List<ProductAttributeValueInsertRequest> productAttributes)
        {
            var entity = await _context.Set<Domain.Product>()
                .Include(i => i.AttributeValues)
                .ThenInclude(i => i.ProductAttribute)
                .SingleOrDefaultAsync(i => i.Id == id);

            if (entity == null)
            {
                return null;
            }

            List<ProductAttributeValue> list = new();
            foreach(var item in productAttributes)
            {
                var attribute = new ProductAttributeValue
                {
                    ProductId = id,
                    ProductAttributeId = item.ProductAttributeId,
                    Value = item.Value
                };

                list.Add(attribute);
            }

            if(list.Count > 0)
            {
                await _context.AddRangeAsync(list);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<ProductResponse>(entity);
        }
    }
}
