namespace EmprestimoLiivroAspCore.Models
{
    public class Item
    {
        public Guid ItemPedidoID { get; set; }

        public int codEmp { get; set; }
        public string codLivro { get; set; }

        public string nomeLivro { get; set; }
        public string imagem { get; set; }
        public string quantidade { get; set; }
    }
}
