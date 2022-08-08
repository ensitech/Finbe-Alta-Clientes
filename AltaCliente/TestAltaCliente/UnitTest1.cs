using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AltaCliente;

namespace AltaCliente.TestAltaCliente
{
    /// <summary>
    /// Descripción resumida de UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        //public UnitTest1()
        //{
        //    //
        //    // TODO: Agregar aquí la lógica del constructor
        //    AltaCliente.InsertCliente plugin = new AltaCliente.InsertCliente();
        //    plugin.insertInAx(new Guid("76F6B2FE-F4CF-EC11-BD3F-0050569D79D0"), CRMLogin.createService());
        //    //C2BA51A2-B41B-E711-83A9-005056851F55
        //}

        //private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la ejecución de pruebas actual.
        ///</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        //#region Atributos de prueba adicionales
        ////
        //// Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        ////
        //// Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        //// [ClassInitialize()]
        //// public static void MyClassInitialize(TestContext testContext) { }
        ////
        //// Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        //// [ClassCleanup()]
        //// public static void MyClassCleanup() { }
        ////
        //// Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        //// [TestInitialize()]
        //// public void MyTestInitialize() { }
        ////
        //// Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        //// [TestCleanup()]
        //// public void MyTestCleanup() { }
        ////
        //#endregion

        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                AltaCliente.InsertCliente plugin = new AltaCliente.InsertCliente();
                plugin.insertInAx(new Guid("2A6292D8-3C7D-EC11-BB6F-0050569D79D0"), CRMLogin.createService());
                //LC00023696 - PF
                //LC00023699 - PM
            }
            catch (Exception ex)
            {

            }
        }
    }
}
