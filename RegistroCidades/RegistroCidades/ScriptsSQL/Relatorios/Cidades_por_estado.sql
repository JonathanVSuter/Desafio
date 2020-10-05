select UF.Sigla,COUNT(*) from UF, Cidade Where UF.ID = Cidade.UF GROUP BY UF.Sigla ;

