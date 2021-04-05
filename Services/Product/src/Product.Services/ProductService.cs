using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Contracts.Requests;
using Product.Contracts.Responses;
using Product.Core.Helpers;
using Product.Core.Interfaces;
using Product.Database;
using Product.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Services
{
    public class ProductService :
        CrudService<ProductResponse, ProductSearchRequest, Domain.Product, ProductInsertRequest, ProductUpdateRequest>,
        IProductService
    {
        private readonly IImageUploadService _imageUploadService;
        public ProductService(IImageUploadService imageUploadService, ProductDbContext context, IMapper mapper, IResponseBuilder<Domain.Product> responseBuilder) 
            : base(context, mapper, responseBuilder)
        {
            _imageUploadService = imageUploadService;
        }

        public override async Task<IResponse> InsertAsync(ProductInsertRequest request)
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

            var response = _mapper.Map<ProductResponse>(entity);
            return new Response<ProductResponse>(response);
        }

        public override async Task<IResponse> UpdateAsync(Guid id, ProductUpdateRequest request)
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

            var response = _mapper.Map<ProductResponse>(entity);
            return new Response<ProductResponse>(response);
        }

        public async Task<IResponse> AddAttributesAsync(Guid id, List<ProductAttributeValueInsertRequest> productAttributes)
        {
            var query = _context.Set<Domain.Product>().AsQueryable();
            query = ApplyIncludes(query);
            query = ApplyExpression(query, i => i.Id == id);
            query = query.AsNoTracking();

            var entity = await query.SingleOrDefaultAsync();

            if (entity == null)
            {
                var errorModel = new ErrorModel()
                {
                    Message = $"Product with the id '{id}' does not exist"
                };

                var errorResponse = new ErrorResponse();
                errorResponse.Errors.Add(errorModel);

                return errorResponse;
            }

            List<ProductAttributeValue> list = new();
            foreach (var item in productAttributes)
            {
                var attribute = new ProductAttributeValue
                {
                    ProductId = id,
                    ProductAttributeId = item.ProductAttributeId,
                    Value = item.Value
                };

                list.Add(attribute);
            }

            if (list.Count > 0)
            {
                await _context.AddRangeAsync(list);
                await _context.SaveChangesAsync();
            }

            var response = _mapper.Map<ProductResponse>(entity);
            return new Response<ProductResponse>(response);
        }

        public async Task<ProductResponse> PatchAttributeAsync(Guid id, Guid attributeValueId, ProductAttributePatchRequest request)
        {
            var query = _context.Set<Domain.Product>().AsQueryable();
            query = ApplyIncludes(query);
            query = ApplyExpression(query, i => i.Id == id);
            query = query.AsNoTracking();

            var entity = await query.SingleOrDefaultAsync();

            if (entity == null)
            {
                return null;
            }

            var attributeValueEntity = await _context.Set<ProductAttributeValue>().FindAsync(attributeValueId);

            if (attributeValueEntity == null)
            {
                return null;
            }

            attributeValueEntity.Value = request.Value;
            _context.Set<ProductAttributeValue>().Update(attributeValueEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(entity);
        }

        public async Task<ProductResponse> DeleteAttributesAsync(Guid id, ProductAttributeValueDeleteRequest request)
        {
            var query = _context.Set<Domain.Product>().AsQueryable();
            query = ApplyIncludes(query);
            query = ApplyExpression(query, i => i.Id == id);
            query = query.AsNoTracking();

            var entity = await query.SingleOrDefaultAsync();

            if (entity == null)
            {
                return null;
            }

            var attributeValues = await _context.Set<ProductAttributeValue>()
                .Where(i => request.AttributeValueIds.Contains(i.Id))
                .ToListAsync();

            if (attributeValues.Count > 0)
            {
                _context.RemoveRange(attributeValues);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<ProductResponse>(entity);
        }

        public async Task<ProductResponse> AddCategoriesAsync(Guid id, List<Guid> request)
        {
            var query = _context.Set<Domain.Product>().AsQueryable();
            query = ApplyIncludes(query);
            query = ApplyExpression(query, i => i.Id == id);
            query = query.AsNoTracking();

            var entity = await query.SingleOrDefaultAsync();

            if (entity == null)
            {
                return null;
            }

            List<ProductCategory> list = new();
            foreach(var item in request)
            {
                var category = await _context.Set<Category>().FindAsync(item);

                if(category == null)
                {
                    return null;
                }

                ProductCategory productCategory = await _context.Set<ProductCategory>().FindAsync(item, id);

                if (productCategory != null)
                {
                    return null;
                }

                productCategory = new ProductCategory
                {
                    ProductId = id,
                    CategoryId = item
                };

                list.Add(productCategory);
            }

            if(list.Count > 0)
            {
                await _context.AddRangeAsync(list);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<ProductResponse>(entity);
        }

        public async Task<ProductResponse> DeleteCategoriesAsync(Guid id, List<Guid> request)
        {
            var query = _context.Set<Domain.Product>().AsQueryable();
            query = ApplyIncludes(query);
            query = ApplyExpression(query, i => i.Id == id);
            query = query.AsNoTracking();

            var entity = await query.SingleOrDefaultAsync();

            if (entity == null)
            {
                return null;
            }

            List<ProductCategory> list = new();
            foreach (var item in request)
            {
                var category = await _context.Set<Category>().FindAsync(item);

                if (category == null)
                {
                    return null;
                }

                ProductCategory productCategory = await _context.Set<ProductCategory>().FindAsync(item, id);

                if (productCategory == null)
                {
                    return null;
                }

                list.Add(productCategory);
            }

            if (list.Count > 0)
            {
                _context.RemoveRange(list);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<ProductResponse>(entity);
        }

        protected override IQueryable<Domain.Product> ApplyIncludes(IQueryable<Domain.Product> query)
        {
            return query
                .Include(i => i.Image)
                .Include(i => i.Categories)
                    .ThenInclude(i => i.Category)
                .Include(i => i.AttributeValues)
                    .ThenInclude(i => i.ProductAttribute);
        }

        protected override IQueryable<Domain.Product> ApplyFilter(IQueryable<Domain.Product> query, ProductSearchRequest search)
        {
            if(!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(i => i.Name.Contains(search.Name));
            }

            query = query.Where(i => i.Price > search.PriceRangeBottom);
            query = query.Where(i => i.Price < search.PriceRangeTop);

            return query;
        }
    }
}
