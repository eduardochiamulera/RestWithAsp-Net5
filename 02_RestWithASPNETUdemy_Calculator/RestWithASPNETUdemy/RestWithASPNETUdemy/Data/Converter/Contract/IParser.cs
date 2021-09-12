﻿using System.Collections.Generic;

namespace RestWithASPNETUdemy.Data.Converter.Contract
{
    public interface IParser<Origem, Destino>
    {
        Destino Parse(Origem origem);

        List<Destino> Parse(List<Origem> origem);
    }
}
