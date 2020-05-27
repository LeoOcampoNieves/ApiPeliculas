using ApiPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.IRepository
{
    public interface ICategoriaRepository
    {
        //Obtenemos todas las categorias
        ICollection<Categoria> GetCategorias();

        // Obtengo solo una categoria
        Categoria GetCategoria(int CategoriaId);

        // Valida si existe la categoria por nombre
        bool ExisteCategoria(string nombre);

        // Valida si existe la categoria por id
        bool ExisteCategoria(int id);

        // Creamos un nuevo registro en la tabla categoria
        bool CrearCategoria(Categoria categoria);

        bool ActualizarCategoria(Categoria categoria);

        bool BorrarCategoria(Categoria categoria);

        bool Guardar();
    }
}
