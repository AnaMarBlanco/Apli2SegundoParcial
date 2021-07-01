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
    public class CobrosBLL
    {
        public static List<Clientes> GetClientes()
        {
            List<Clientes> lista = new List<Clientes>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Clientes.ToList(); 
            }
            catch(Exception e)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static bool Guardar(Cobros cobro)
        {
            Contexto contexto = new Contexto();

            bool paso = false;
            try
            {
                if (cobro.CobroId != 0)
                    paso = Modificar(cobro);
                else
                    paso = Insertar(cobro);
            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }

        public static bool Insertar(Cobros cobro)
        {
            Contexto contexto = new Contexto();

            bool paso = false;
            try
            {
                contexto.Cobros.Add(cobro);
                if (paso)
                {
                    foreach (var item in cobro.Detalle)
                    {
                        var venta = VentasBLL.Buscar(item.VentaId);
                        if (venta != null)
                        {
                            venta.Balance -= item.Monto;
                            VentasBLL.Modificar(venta);
                        }
                    }
                }

                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }


        public static bool Modificar(Cobros cobro)
        {
            Contexto contexto = new Contexto();

            bool paso = false;
            try
            {
                contexto.Database.ExecuteSqlRawAsync($"DELETE FROM CobrosDetalle WHERE CobroId = {cobro.CobroId}");
                foreach (var cobroDetalle in cobro.Detalle)
                {
                    contexto.Entry(cobro).State = EntityState.Added;
                }
                contexto.Entry(cobro).State = EntityState.Modified;

                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }



        public static Cobros Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Cobros cobro = new Cobros();
            bool paso = false;
            try
            {
                if (id != 0)
                {
                    cobro = contexto.Cobros.Include(c => c.Detalle).Where(v => v.CobroId == id).FirstOrDefault();
                }

            }
            catch (Exception)
            {

                throw;
            }

            return cobro;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            Cobros cobro = new Cobros();

            try
            {
                cobro = contexto.Cobros.Find(id);
                contexto.Cobros.Remove(cobro);
                if (paso)
                {
                    foreach (var cobroDetalle in cobro.Detalle)
                    {
                        var venta = VentasBLL.Buscar(cobroDetalle.VentaId);
                        if (venta != null)
                        {
                            venta.Balance += cobroDetalle.Monto;
                            VentasBLL.Modificar(venta);
                        }
                    }
                }
                paso = contexto.SaveChanges() > 0;


            }
            catch (Exception)
            {

                throw;
            }

            return paso;
        }

        public static List<Cobros> GetList(Expression<Func<Cobros, bool>> expression)
        {
            Contexto contexto = new Contexto();
            List<Cobros> list = new List<Cobros>();
            try
            {
                list = contexto.Cobros.Where(expression).ToList();
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
