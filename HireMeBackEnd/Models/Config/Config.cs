using HireMeBackEnd.Services.ProductConsolidator.Collect;
using HireMeBackEnd.Services.ProductConsolidator.Deposit;

namespace HireMeBackEnd.Models.Config;

public class Config
{
    public Collect Collect { get; set; }
    public Deposit Deposit { get; set; }
}

public class Collect
{
    public CollectStrategy Strategy { get; set; }
}

public class Deposit
{
    public DepositStrategy Strategy { get; set; }
}
