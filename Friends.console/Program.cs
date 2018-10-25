using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using viavarejofriends;

namespace viavarejofriends
{
    public class Program
    {
        static void Main(string[] args)
        {
            string nomeAmigo = string.Empty;
            string sobrenomeAmigo = string.Empty;
            int opcao;

            do
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("[ 1 ] Escolha amigo");
                Console.WriteLine("[ 2 ] Lista de amigos cadastrados");
                Console.WriteLine("[ 0 ] Sair do Software");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();

                Console.Write("Digite uma opção: ");
                opcao = Int32.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        escolhaAmigo(ref nomeAmigo, ref sobrenomeAmigo);
                        break;
                    case 2:
                        listaAmigos();
                        break;
                    default:
                        saiPrograma();
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
            while (opcao != 0);

        }

        private static void listaAmigos()
        {
            List<Person> persons = Repository.GetPersons();

            Console.Clear();
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("*******************| LITA DE AMIGOS |**************************");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();

            foreach (Person item in persons)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------------");
                Console.WriteLine("Amigo: {0} {1} | Localidade: {2}", item.nome, item.sobrenome, item.endereco.cidade);
            }

            Console.WriteLine();
            Console.WriteLine("-------------------fim da Lista------------------------------");
        }

        private static void saiPrograma()
        {
            Console.WriteLine();
            Console.WriteLine("Até mais, vc saiu do Programa. Clique qq tecla para sair...");
        }

        private static void listaAmigosProximos(Person porigem, List<Person> pamigosproximos)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("***************| LITA DE AMIGOS PRÓXIMOS|**********************");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Amigo : {0} {1}", porigem.nome, porigem.sobrenome);
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();

            foreach (Person item in pamigosproximos)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------");
                Console.WriteLine("{0} {1} | Localidade: {2}", item.nome, item.sobrenome, item.endereco.cidade);
                Console.WriteLine("-------------------------------------------------------------------------------------");
            }

            Console.WriteLine();
            Console.WriteLine("-------------------fim da Lista------------------------------");
        }

        private static void escolhaAmigo(ref string nomeAmigo, ref string sobrenomeAmigo)
        {
            Console.Clear();
            do
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("******************| ESCOLHA SEU AMIGO |*************************");
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine();

                Console.Write("Digite a nome do seu amigo: ");
                nomeAmigo = Console.ReadLine();

                Console.Write("Digite a sobrenome do seu amigo: ");
                sobrenomeAmigo = Console.ReadLine();

                Console.WriteLine();
                Console.Write("******Aguarde processando localização!******");

                if (nomeAmigo != string.Empty)
                {
                    Person porigem = Repository.GetPersonByName(nomeAmigo, sobrenomeAmigo);
                    List<Person> pamigosproximos = Repository.GetCloseFriends(nomeAmigo, sobrenomeAmigo);
                    listaAmigosProximos(porigem, pamigosproximos);
                }

            }
            while (nomeAmigo == string.Empty);
        }


    }
}
