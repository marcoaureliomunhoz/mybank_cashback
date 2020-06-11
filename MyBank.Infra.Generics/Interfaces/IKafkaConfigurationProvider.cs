using MyBank.Infra.Generics.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Infra.Generics.Interfaces
{
    public interface IKafkaConfigurationProvider
    {
        KafkaConfiguration Get();
    }
}
