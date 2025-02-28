﻿using AutoMapper;
using MaxiShop.Application.DTO.Category;
using MaxiShop.Application.DTO.Product;
using MaxiShop.Application.InputModels;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Application.ViewModels;
using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService<ProductDto, Product> _paginationService;
        public ProductService(IProductRepository productRepository, IMapper mapper, IPaginationService<ProductDto, Product> paginationService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _paginationService= paginationService;
        }
        

        public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var createdEntity = await _productRepository.CreateAsync(product);
            var entity = _mapper.Map<ProductDto>(createdEntity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(x => x.Id == id);
            await _productRepository.DeleteAsync(product);
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetAllByFilterAsync(int? categoryId, int? brandId)
        {
            var data = await _productRepository.GetAllProductsAsync();
            IEnumerable<Product> query = data;
           
            if (categoryId > 0)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }
            if (brandId > 0)
            {
                query = query.Where(query => query.BrandId == brandId);
            }
            var result = _mapper.Map<List<ProductDto>>(query);
            return result;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetDetailsAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PaginationVM<ProductDto>> GetPagination(PaginationInputModel pagination)
        {
            var source =await _productRepository.GetAllProductsAsync();
            var result = _paginationService.GetPagination(source, pagination);
            return result;
        }

        public async Task UpdateAsync(UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            await _productRepository.UpdateAsync(product);
        }
    }
}
