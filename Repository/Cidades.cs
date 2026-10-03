using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Projetos_API.Models;
using System.IO;
using System.Text;

namespace Projetos_API.Repository
{
    public static class CidadesRepository
    {
        private static List<Cidade> cidades;
        private const string EnderecoJson = "C:\\Users\\Pichau\\Desktop\\Projetos Api\\cidades.json";

        public static List<Cidade> Cidades {get
            {
                if (cidades == null)
                {
                    string jsonString = File.ReadAllText(EnderecoJson);
                    if (!string. IsNullOrEmpty(jsonString))
                    {
                        cidades = JsonSerializer.Deserialize<List<Cidade>>(jsonString);
                    }
                    else
                    {
                        CarregarCidades();
                    }

                    return cidades;
                }
                else
                {
                    return cidades;
                }
            }
        }

        private static void CarregarCidades()
        {
            cidades = new List<Cidade>()
            {
                new Cidade()
                {
                    Id = 100,
                    Nome = "Santos",
                    IdEstado = 11,
                    IdPais = 55,
                    Populacao = 10000
                },
                new Cidade()
                {
                    Id = 200,
                    Nome = "São Vicente",
                    IdEstado = 11,
                    IdPais = 55,
                    Populacao = 20000
                },
                new Cidade()
                {
                    Id = 300,
                    Nome = "Belo Horizonte",
                    IdEstado = 31,
                    IdPais = 55,
                    Populacao = 30000
                }

            };
        }

        public static void Save()
        {
            string jsonString = JsonSerializer.Serialize(cidades);
            File.WriteAllText(EnderecoJson, jsonString, Encoding.UTF8);
        }
    }
}