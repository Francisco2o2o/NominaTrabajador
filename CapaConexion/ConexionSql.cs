﻿using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace CapaConexion
{
    public class ConexionSql : IDisposable
    {
        public SqlConnection strConexion;
        public SqlTransaction myTrans;
        private SqlCommand cmdComando;
        private bool bActiva = false;
        private string IsCadena = "";
        private int ntimeOut;

        public ConexionSql(string psBaseDatos = "", int pnTimeOut = 7200)
        {
            LeerIni(psBaseDatos);
            strConexion = new SqlConnection(IsCadena);
            strConexion.Open();
            ntimeOut = pnTimeOut;
        }

        private bool LeerIni(string psBaseDatos)
        {
            bool bResultado = false;
            string IsIni = "ConexionSql.txt"; 
            try
            {
                CrearArchivoSiNoExiste(IsIni); 
                IsCadena = LeerArchivo(psBaseDatos, IsIni);
                bResultado = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer el archivo de configuración: {ex.Message}");
            }
            return bResultado;
        }

        

        private string LeerArchivo(string psBaseDatos, string IsIni)
        {
            int k = 0;
            string Pass = "";
            string Usuario = "";
            string BaseDatos = "";
            string IsCadenaCon = "";
            string cArchivo = "";
            string cClave1 = "";
            string cClave2 = "";
            string sLine = "";
            string Source = "";
            try
            {
                cArchivo = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                if (cArchivo.Substring(cArchivo.Length - 1, 1) == "\\")
                {
                    cArchivo = cArchivo + IsIni;
                }
                else
                {
                    cArchivo = cArchivo + @"\" + IsIni;
                }
                using (StreamReader objReader = new StreamReader(cArchivo, Encoding.Default))
                {
                    do
                    {
                        sLine = objReader.ReadLine();
                        if (sLine != null)
                        {
                            k = sLine.IndexOf("=");
                            if (k > 0)
                            {
                                cClave1 = sLine.Substring(0, k);
                                cClave2 = sLine.Substring(k + 1, sLine.Length - (k + 1));
                                if (cClave1 == "Server")
                                {
                                    Source = cClave2;
                                }
                                else if (cClave1 == "DataBase")
                                {
                                    if (psBaseDatos == "")
                                    {
                                        psBaseDatos = cClave2;
                                    }
                                    BaseDatos = psBaseDatos;
                                }
                                else if (cClave1 == "Password")
                                {
                                    Pass = cClave2;
                                }
                                else if (cClave1 == "User")
                                {
                                    Usuario = cClave2;
                                }
                            }
                        }
                    } while (sLine != null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (Pass.Trim() == "" || Usuario.Trim() == "")
                IsCadena = "Integrated Security=true;INITIAL CATALOG=" + ((BaseDatos.Trim())) + ";DATA SOURCE=" + ((Source.Trim())) + "";
            else
                IsCadena = "Database=" + BaseDatos.Trim() + ";Data Source=" + Source.Trim() + ";User Id=" + Usuario.Trim() + ";Password=" + Pass.Trim() + ";TrustServerCertificate=True;";
            IsCadenaCon = IsCadena;
            return IsCadenaCon;
        }
        private void CrearArchivoSiNoExiste(string nombreArchivo)
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombreArchivo);

            if (!File.Exists(rutaArchivo))
            {
                try
                {
                    string contenido = "Server=DESKTOP-D4F7FGN\n" +
                                       "DataBase=DBNominaTrabajador\n" +
                                       "User=sa\n" +
                                       "Password=123456";

                    File.WriteAllText(rutaArchivo, contenido, Encoding.Default);
                }
                catch (Exception ex)
                {
                    throw new Exception($"No se pudo crear el archivo {nombreArchivo}: {ex.Message}");
                }
            }
        }

        public int EjecutarProcedimiento(string storedProcedureName, params object[] parameterValues)
        {
            int nRowsAfec = 0;
            try
            {
                if (bActiva)
                {
                    cmdComando = new SqlCommand(storedProcedureName, strConexion, myTrans);
                    cmdComando.CommandTimeout = ntimeOut;
                }
                else
                {
                    cmdComando = new SqlCommand(storedProcedureName, strConexion);
                    cmdComando.CommandTimeout = ntimeOut;
                }
                cmdComando.CommandType = CommandType.StoredProcedure;
                AssignParameterValues(cmdComando, parameterValues);
                nRowsAfec = cmdComando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return nRowsAfec;
        }

        private void AssignParameterValues(DbCommand command, object[] values)
        {
            foreach (IDataParameter parameter in values)
            {
                command.Parameters.Add(parameter);
            }
        }

        public DataSet EjecutarProcedimientoDS(string storeProcedureName, params object[] parameterValues)
        {
            DataSet dsResultado = new DataSet();
            SqlDataAdapter da;
            try
            {
                if (bActiva)
                {
                    cmdComando = new SqlCommand(storeProcedureName, strConexion, myTrans);
                    cmdComando.CommandTimeout = ntimeOut;
                }
                else
                {
                    cmdComando = new SqlCommand(storeProcedureName, strConexion);
                    cmdComando.CommandTimeout = ntimeOut;
                }
                cmdComando.CommandType = CommandType.StoredProcedure;
                AssignParameterValues(cmdComando, parameterValues);
                da = new SqlDataAdapter(cmdComando);
                da.Fill(dsResultado);
                return dsResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable EjecutarProcedimientoDT(string storedProcedureName, params object[] parameterValues)
        {
            DataTable dtResultado = new DataTable();
            SqlDataAdapter da;
            try
            {
                if (bActiva)
                {
                    cmdComando = new SqlCommand(storedProcedureName, strConexion, myTrans);
                    cmdComando.CommandTimeout = ntimeOut;
                }
                else
                {
                    cmdComando = new SqlCommand(storedProcedureName, strConexion);
                    cmdComando.CommandTimeout = ntimeOut;
                }
                cmdComando.CommandType = CommandType.StoredProcedure;
                if (parameterValues != null)
                {
                    AssignParameterValues(cmdComando, parameterValues);
                }
                da = new SqlDataAdapter(cmdComando);
                da.Fill(dtResultado);
                return dtResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CierraConexion()
        {
            if (strConexion != null && strConexion.State == ConnectionState.Open)
            {
                strConexion.Close();
            }
        }

        public void BeginT()
        {
            myTrans = strConexion.BeginTransaction();
            bActiva = true;
        }

        public void Commit()
        {
            myTrans?.Commit();
            bActiva = false;
        }

        public void RollBackT()
        {
            myTrans?.Rollback();
            bActiva = false;
        }
        public void Dispose()
        {
            CierraConexion();
            strConexion?.Dispose();
            cmdComando?.Dispose();
            myTrans?.Dispose();
        }
    }
}
