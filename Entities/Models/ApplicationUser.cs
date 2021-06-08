﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [MaxLength(50)]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [PersonalData]
        [MaxLength(40)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [PersonalData]
        [MaxLength(9)]
        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [PersonalData]
        [MaxLength(255)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [PersonalData]
        [Display(Name = "Reputação")]
        public int Reputacao { get; set; }

        [PersonalData]
        [Display(Name = "Produtos a venda")]
        public int ProdutosAVenda { get; set; }

        [PersonalData]
        [Display(Name = "Produtos aguardando aprovação de venda")]
        public int ProdutosAguardandoApVenda { get; set; }

        [PersonalData]
        [Display(Name = "Produtos vendidos")]
        public int ProdutosVendido { get; set; }

        [PersonalData]
        [Display(Name = "Produtos em rota de entrega")]
        public int ProdutosEmRotaDeEntrega { get; set; }

        [PersonalData]
        [Display(Name = "Produtos entregue")]
        public int ProdutosEntregue { get; set; }

        [PersonalData]
        [Display(Name = "Produtos bloqueados")]
        public int ProdutosBloqueado { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
