using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace HTTP
{
    class Program
    {
        enum Menu { Lista = 1, Unica, Sair}

        static void Main(string[] args)
        {
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Bem Vindo, escolha a opção desejada: ");
                Console.WriteLine("1-Lista de requisição\n2-Requisição unica\n3-Sair");
                int opInt = int.Parse(Console.ReadLine());
                

                if (opInt > 0 && opInt <=3 )
                {
                    Menu Escolha = (Menu)opInt;
                    switch (Escolha)
                    {
                        case Menu.Lista:
                            reqLista();
                            break;
                        case Menu.Unica:
                            reqUnica();
                            break;
                        case Menu.Sair:
                            escolheuSair = true;
                            break;
                    }
                    Console.Clear();
                }
                else
                {
                    escolheuSair = true;
                }
               

            }
           
        }

        static void reqLista()
        {
            var requisicao = WebRequest.Create("https://jsonplaceholder.typicode.com/todos");
            requisicao.Method = "GET";

            using (var resposta = requisicao.GetResponse())
            {
                var stream = resposta.GetResponseStream();

                StreamReader leitor = new StreamReader(stream);
                object dados = leitor.ReadToEnd();

                List<Tarefa> tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(dados.ToString());

                foreach (Tarefa tarefa in tarefas)
                {
                    tarefa.Exibir();
                }

                stream.Close();
                resposta.Close();

            }
            Console.ReadLine();
        }

        static void reqUnica()
        {
            var requisicao = WebRequest.Create("https://jsonplaceholder.typicode.com/todos/5");
            requisicao.Method = "GET";

            using (var resposta = requisicao.GetResponse())
            {
                var stream = resposta.GetResponseStream();
                StreamReader leitor = new StreamReader(stream);
                object dados = leitor.ReadToEnd();

                Tarefa tarefa = JsonConvert.DeserializeObject<Tarefa>(dados.ToString());


                tarefa.Exibir();


                stream.Close();
                resposta.Close();

            }
            Console.ReadLine();
        }
    }
}
