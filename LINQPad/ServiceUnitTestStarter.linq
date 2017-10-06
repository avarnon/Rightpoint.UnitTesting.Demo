var type = typeof(MyService);
var constructor = type.GetConstructors().Single();
var parms = constructor.GetParameters();

var sb = new StringBuilder();

foreach (var currentParm in parms.OrderBy(p => p.Name))
{
    sb.AppendLine($"        private Mock<{currentParm.ParameterType.Name}> _{currentParm.Name};");
}
sb.AppendLine();

sb.AppendLine($"        [TestInitialize]");
sb.AppendLine($"        public void TestInitialize()");
sb.AppendLine($"        {{");
foreach (var currentParm in parms.OrderBy(p => p.Name))
{
    sb.AppendLine($"            _{currentParm.Name} = new Mock<{currentParm.ParameterType.Name}>();");
}
sb.AppendLine($"        }}");
sb.AppendLine();


for (var i = 0; i < parms.Length; i++)
{
    var targetParm = parms[i];
    sb.AppendLine($"        [TestMethod]");
    sb.AppendLine($"        [ExpectedException(typeof(ArgumentNullException))]");
    sb.AppendLine($"        public void {type.Name}_Constructor_{targetParm.ParameterType.Name.Substring(1)}_Null()");
    sb.AppendLine($"        {{");
    sb.AppendLine($"            var service = new {type.Name}(");
    for (var j = 0; j < parms.Length; j++)
    {
        if (j == i)
        {
            sb.Append($"                null");
        }
        else
        {
            var currentParm = parms[j];
            sb.Append($"                _{currentParm.Name}.Object");
        }

        if (j == parms.Length - 1)
        {
            sb.AppendLine($");");
        }
        else
        {
            sb.AppendLine($",");
        }
    }

    sb.AppendLine($"            Assert.Fail(\"Expected exception was not thrown\");");
    sb.AppendLine($"        }}");
    sb.AppendLine();
}

sb.AppendLine($"        [TestMethod]");
sb.AppendLine($"        public void {type.Name}_Constructor_Valid()");
sb.AppendLine($"        {{");
sb.AppendLine($"            var service = new {type.Name}(");
for (var j = 0; j < parms.Length; j++)
{
    var currentParm = parms[j];
    sb.Append($"                _{currentParm.Name}.Object");

    if (j == parms.Length - 1)
    {
        sb.AppendLine($");");
    }
    else
    {
        sb.AppendLine($",");
    }
}

sb.AppendLine($"        }}");
sb.AppendLine();


sb.ToString().Dump();