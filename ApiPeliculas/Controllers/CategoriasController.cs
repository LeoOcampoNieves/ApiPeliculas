﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/Categorias")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _ctRepo;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;

            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctRepo.GetCategorias();

            var listaCategoriasDto = new List<CategoriaDto>();

            foreach (var lista in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(lista));
            }

            return Ok(listaCategoriasDto);
        }

        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        public IActionResult GetCategoria(int categoriaId)
        {
            var itemCategoria = _ctRepo.GetCategoria(categoriaId);

            if(itemCategoria == null)
            {
                return NotFound();
            }

            var itemCategoriaDto = _mapper.Map<CategoriaDto>(itemCategoria);

            return Ok(itemCategoriaDto);
        }

        [HttpPost]
        public IActionResult CrearCategoria([FromBody] CategoriaDto categoriaDto)
        {
            if(categoriaDto == null)
            {
                return BadRequest(ModelState);
            }

            if(_ctRepo.ExisteCategoria(categoriaDto.Nombre))
            {
                ModelState.AddModelError("", "La categoría ya existe");

                return StatusCode(404, ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            if(!_ctRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro{categoria.Nombre}");

                return StatusCode(500, ModelState);
            }

            // Devuelve el ultimo registro creado
            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria);
        }

        [HttpPatch("{categoriaId:int}", Name = "ActualizarCategoria")]
        public IActionResult ActualizarCategoria(int categoriaId, [FromBody] CategoriaDto categoriaDto)
        {
            if(categoriaDto == null || categoriaId != categoriaDto.Id) 
            {
                return BadRequest(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            if (!_ctRepo.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro{categoria.Nombre}");

                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}