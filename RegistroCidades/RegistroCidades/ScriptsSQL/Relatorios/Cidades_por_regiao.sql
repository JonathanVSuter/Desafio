select Regiao.Descricao,COUNT(*) from Regiao, Cidade Where Regiao.ID = Cidade.Regiao GROUP BY Regiao.Descricao  ;

