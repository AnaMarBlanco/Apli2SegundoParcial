using Apli2SegundoParcial.DAL;
using Apli2SegundoParcial.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Apli2SegundoParcial.BLL
{
    public class VentasBLL
    {
        public static bool Guardar(Ventas venta)
        {
            Contexto contexto = new Contexto();

            bool paso = false;
            try
            {
                if (venta.VentaId != 0)
                    paso = Modificar(venta);
                else
                    paso = Insertar(venta);
            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }

        public static bool Insertar(Ventas venta)
        {
            Contexto contexto = new Contexto();

            bool paso = false;
            try
            {
                contexto.Ventas.Add(venta);

                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }


        public static bool Modificar(Ventas venta)
        {
            Contexto contexto = new Contexto();

            bool paso = false;
            try
            {
                contexto.Entry(venta).State = EntityState.Modified;

                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }

        public static Ventas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Ventas venta = new Ventas();
            bool paso = false;
            try
            {
                if (id != 0)
                {
                    venta = contexto.Ventas.Find(id);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return venta;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            Ventas venta = new Ventas();

            try
            {
                venta = contexto.Ventas.Find(id);
                contexto.Ventas.Remove(venta);
                paso = contexto.SaveChanges() > 0;


            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }

        public static List<Ventas> GetList(Expression<Func<Ventas, bool>> expression)
        {
            Contexto contexto = new Contexto();
            List<Ventas> list = new List<Ventas>();
            try
            {
                list = contexto.Ventas.Where(expression).ToList();
                //Calcular();
            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
    }
}
