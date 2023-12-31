﻿using AutoMapper;
using CasgemMicroservice.Services.Catalog.Dtos.CategoryDtos;
using CasgemMicroservice.Services.Catalog.Models;
using CasgemMicroservice.Services.Catalog.Settings;
using CasgemMicroservice.Shared.Dtos;
using MongoDB.Driver;

namespace CasgemMicroservice.Services.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings _dataBaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(_dataBaseSettings.ConnectionString);
            var database = client.GetDatabase(_dataBaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_dataBaseSettings.CategoryCollectionName);
        }

        public async Task<Response<NoContent>> CreateCategoryAsync(CreateCategoryDto createCategory)
        {
            var value = _mapper.Map<Category>(createCategory);
            await _categoryCollection.InsertOneAsync(value);
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteCategoryAsync(string id)
        {
            var value = await _categoryCollection.DeleteOneAsync(x => x.CategoryID == id);
                return Response<NoContent>.Success(204);
        }

        public async Task<Response<List<ResultCategoryDto>>> GetAllCategoryListAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            return Response<List<ResultCategoryDto>>.Success(_mapper.Map<List<ResultCategoryDto>>(values), 200);
        }

        public async Task<Response<ResultCategoryDto>> GetByIdCategoryAsync(string id)
        {
            var value = await _categoryCollection.Find<Category>(x => x.CategoryID == id).FirstOrDefaultAsync();
            if (value == null)
            {
                return Response<ResultCategoryDto>.Fail("kategori bulunmadı", 404);
            }
            else
            {
                return Response<ResultCategoryDto>.Success(_mapper.Map<ResultCategoryDto>(value), 200);
            }
        }

        public async Task<Response<NoContent>> UpdateCategoryAsync(UpdateCategoryDto updateCategory)
        {
            var values = _mapper.Map<Category>(updateCategory);
            var result = await _categoryCollection.FindOneAndReplaceAsync(x =>
            x.CategoryID == updateCategory.CategoryID, values);
            if (result == null)
            {
                return Response<NoContent>.Fail("Güncellenecek veri bulunumadı", 400);
            }
                return Response<NoContent>.Success(204);
        }
    }
}