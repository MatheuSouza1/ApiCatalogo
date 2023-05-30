﻿using ApiCatalogo.Context;
using ApiCatalogo.DTOs.Mapping;
using ApiCatalogo.Entities;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCatalogo.DTOs;
using ApiCatalogo.Pagination;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly IUnitOfWork _UoW;
        public readonly IMapper _Mapping;
        public CategoriesController(IUnitOfWork context, IMapper mapping)
        {
            _UoW = context;
            _Mapping = mapping;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get([FromQuery] CategoryParameters categoryParameters)
        {
            var categorys = _UoW.CategoryRepository.GetCategories(categoryParameters);
            if (categorys == null)
            {
                return NotFound();
            }

            var metadata = new
            {
                categorys.TotalCount,
                categorys.PageSize,
                categorys.CurrentPage,
                categorys.TotalPages,
                categorys.HasNext,
                categorys.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categorysDTO = _Mapping.Map<List<CategoryDTO>>(categorys);
            return categorysDTO;
        }

        [HttpGet("Products")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategoryProducts()
        {
            var category = _UoW.CategoryRepository.GetAll().ToList();
            var categorysDTO = _Mapping.Map<List<CategoryDTO>>(category);
            return categorysDTO;
        }

        [HttpPost]
        public ActionResult Post(CategoryDTO categoryDTO)
        {
            var category = _Mapping.Map<Category>(categoryDTO);
            _UoW.CategoryRepository.Add(category);
            _UoW.Commit();
            var categoryDto = _Mapping.Map<CategoryDTO>(categoryDTO);
            return new CreatedAtRouteResult("Objeto", new { id = category.CategoryId, categoryDto });
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, CategoryDTO categoryDto)
        {
            //verificando se o id passado é igual ao passado no body request
            if (categoryDto.CategoryId != id)
            {
                return BadRequest();
            }
            var category = _Mapping.Map<Category>(categoryDto);
            _UoW.CategoryRepository.Update(category);
            _UoW.Commit();
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var category = _UoW.CategoryRepository.GetById(category => category.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            _UoW.CategoryRepository.Delete(category);
            _UoW.Commit();
            return Ok();
        }
    }
}