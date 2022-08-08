using AltaCliente.WsDynamicsAx;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace AltaCliente
{
    public class InsertCliente : IPlugin
    {
        private string organizacion;
        //private string DomainLogonName;
        private string DomainLogonName = "adpeco\\ravilama";

        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                var Entidad = (Entity)context.InputParameters["Target"];
                this.organizacion = context.OrganizationName;

                IOrganizationServiceFactory ICrm = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService serviceProxy = ICrm.CreateOrganizationService(context.UserId);

                try
                {
                    Guid idLineaCredito = Entidad.Id;
                    this.setDomainLogonName(serviceProxy, context);
                    this.insertInAx(idLineaCredito, serviceProxy);
                }
                catch (SoapException ex)
                {
                    throw new InvalidPluginExecutionException("Error: " + ex.Message, (Exception)ex);
                }
                catch (Exception ex)
                {
                    throw new InvalidPluginExecutionException("Error: " + ex.Message);
                }
            }
        }

        public void insertInAx(Guid idLineaCredito, IOrganizationService servicio)
        {
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "Dirección Fiscal";
            string str6 = "Dirección Adicional 1";
            string str7 = "Dirección Adicional 2";
            string str8 = "";
            string str9 = "";
            string str10 = "";
            string str11 = "";
            string str12 = "";
            string[] atributos1 = new string[11]
                  {
                    "fib_lineadecreditoid",
                    "fib_customerpfid",
                    "fib_customerpmid",
                    "fib_name",
                    "fib_importedisponible",
                    "fib_importeapertura",
                    "fib_monedaid",
                    "fib_valorgarantiaprincipal",
                    "fib_disponer",
                    "fib_diferenciaimporte",
                    "fib_estatus"
                  };
            string str13 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_lineadecredito'>          <attribute name='fib_lineadecreditoid' />          <attribute name='fib_customerpfid' />          <attribute name='fib_customerpmid' />          <attribute name='fib_name' />          <attribute name='fib_importedisponible' />          <attribute name='fib_importeapertura' />          <attribute name='fib_monedaid' />          <attribute name='fib_valorgarantiaprincipal' />          <attribute name='fib_disponer' />          <attribute name='fib_diferenciaimporte' />          <attribute name='fib_estatus' />\t\t     <filter type='and'>              <condition attribute='fib_lineadecreditoid' operator='eq' value='" + idLineaCredito.ToString() + "' />          </filter>      </entity>  </fetch>";

            var request = new ExecuteFetchRequest { FetchXml = str13 };
            var response = (ExecuteFetchResponse)servicio.Execute(request);

            string str14 = response.FetchXmlResult;

            List<Hashtable> list1 = this.XmlToMap(str14, atributos1);
          //  if (list1[0][(object)"fib_estatus"] == null || !(list1[0][(object)"fib_estatus"].ToString() == "1"))
          //      return;
            string str15;
            string str16;
            string str17;
            string str18;
            string str19;
            string str20;
            string str21;
            string str22;
            string str23;
            string str24;
            string str25;
            string str26;
            string str27;
            string str28;
            string str29;
            string str30;
            string str31;
            string str32;
            string str42 = "MX";
            if (list1[0][(object)"fib_customerpmid"] != null)
            {
                Guid guid1 = new Guid(list1[0][(object)"fib_customerpmid"].ToString());
                string[] atributos2 = new string[24]
                {
                  "accountnumber",
                  "name",
                  "fib_rfc",
                  "address1_line1",
                  "fib_coloniaid",
                  "address1_postalcode",
                  "address2_line1",
                  "fib_coloniaid2",
                  "address2_postalcode",
                  "fib_address3_line1",
                  "fib_coloniaid3",
                  "fib_address3_postalcode",
                  "fib_apellidopaterno",
                  "fib_apellidomaterno",
                  "address1_primarycontactname",
                  "fib_segundonombre",
                  "telephone3",
                  "fax",
                  "emailaddress1",
                  "fib_impuestoaplicable",
                  "fib_formadepagoid",
                  "fib_digitos",
                  "fib_correoelectronicoadicional",
                  "fib_tipodepersonamoral"
                };
                string str33 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='account'>          <attribute name='accountnumber' />          <attribute name='name' />          <attribute name='fib_rfc' />          <attribute name='address1_line1' />          <attribute name='fib_coloniaid' />          <attribute name='address1_postalcode' />          <attribute name='address2_line1' />          <attribute name='fib_coloniaid2' />          <attribute name='address2_postalcode' />          <attribute name='fib_address3_line1' />          <attribute name='fib_coloniaid3' />          <attribute name='fib_address3_postalcode' />          <attribute name='fib_apellidopaterno' />          <attribute name='fib_apellidomaterno' />          <attribute name='address1_primarycontactname' />          <attribute name='fib_segundonombre' />          <attribute name='telephone3' />          <attribute name='fax' />          <attribute name='emailaddress1' />          <attribute name='fib_impuestoaplicable' />          <attribute name='fib_formadepagoid' />          <attribute name='fib_digitos' />          <attribute name='fib_correoelectronicoadicional' />          <attribute name='fib_tipodepersonamoral' />\t\t     <filter type='and'>              <condition attribute='accountid' operator='eq' value='" + guid1.ToString() + "' />          </filter>      </entity>  </fetch>";

                request = new ExecuteFetchRequest { FetchXml = str33 };
                response = (ExecuteFetchResponse)servicio.Execute(request);

                List<Hashtable> list2 = this.XmlToMap(response.FetchXmlResult, atributos2);
                str15 = list2[0][(object)"accountnumber"].ToString();
                if (list2[0][(object)"fib_tipodepersonamoral"] != null)
                {
                    if (list2[0][(object)"fib_tipodepersonamoral"].ToString() == "1")
                        str4 = "04";
                    else if (list2[0][(object)"fib_tipodepersonamoral"].ToString() == "2")
                        str4 = "05";
                    else if (list2[0][(object)"fib_tipodepersonamoral"].ToString() == "3")
                        str4 = "06";
                }
                else
                {
                    str4 = "04";
                }
                str16 = list2[0][(object)"name"] != null ? list2[0][(object)"name"].ToString() : "";
                str17 = list2[0][(object)"fib_rfc"] != null ? list2[0][(object)"fib_rfc"].ToString() : "";
                str18 = list2[0][(object)"accountnumber"] != null ? list2[0][(object)"accountnumber"].ToString() : "";
                str19 = list2[0][(object)"address1_line1"] != null ? list2[0][(object)"address1_line1"].ToString() : "";
                str20 = list2[0][(object)"address2_line1"] != null ? list2[0][(object)"address2_line1"].ToString() : "";
                str21 = list2[0][(object)"address3_line1"] != null ? list2[0][(object)"address3_line1"].ToString() : "";
                str22 = list2[0][(object)"address1_postalcode"] != null ? list2[0][(object)"address1_postalcode"].ToString() : "";
                str23 = list2[0][(object)"address2_postalcode"] != null ? list2[0][(object)"address2_postalcode"].ToString() : "";
                str24 = list2[0][(object)"address3_postalcode"] != null ? list2[0][(object)"address3_postalcode"].ToString() : "";
                str25 = (list2[0][(object)"fib_apellidopaterno"] != null ? list2[0][(object)"fib_apellidopaterno"].ToString() : "") + " " + (list2[0][(object)"fib_apellidomaterno"] != null ? list2[0][(object)"fib_apellidomaterno"].ToString() : "") + " " + (list2[0][(object)"address1_primarycontactname"] != null ? list2[0][(object)"address1_primarycontactname"].ToString() : "") + " " + (list2[0][(object)"fib_segundonombre"] != null ? list2[0][(object)"fib_segundonombre"].ToString() : "");
                str26 = list2[0][(object)"telephone3"] != null ? list2[0][(object)"telephone3"].ToString() : "";
                str27 = list2[0][(object)"fax"] != null ? list2[0][(object)"fax"].ToString() : "";
                str28 = list2[0][(object)"emailaddress1"] != null ? list2[0][(object)"emailaddress1"].ToString() : "";
                str29 = list2[0][(object)"fib_impuestoaplicable"] != null ? (list2[0][(object)"fib_impuestoaplicable"].ToString() == "4" ? "IVAEXENTO" : "IVAGRAL") : "IVAGRAL";
                Guid guid2 = new Guid(list2[0][(object)"fib_coloniaid"].ToString());
                string[] atributos3 = new string[1]
                        {
                          "fib_name"
                        };
                string str34 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_colonia'>          <attribute name='fib_name' />\t\t     <filter type='and'>              <condition attribute='fib_coloniaid' operator='eq' value='" + guid2.ToString() + "' />          </filter>      </entity>  </fetch>";

                request = new ExecuteFetchRequest { FetchXml = str34 };
                response = (ExecuteFetchResponse)servicio.Execute(request);

                string str35 = this.XmlToMap(response.FetchXmlResult, atributos3)[0][(object)"fib_name"].ToString();
                str30 = str35.Substring(str35.IndexOf("-") + 1, str35.Length - (str35.IndexOf("-") + 1));
                if (list2[0][(object)"fib_coloniaid2"] != null)
                {
                    guid2 = new Guid(list2[0][(object)"fib_coloniaid2"].ToString());
                    string str36 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_colonia'>          <attribute name='fib_name' />\t\t     <filter type='and'>              <condition attribute='fib_coloniaid' operator='eq' value='" + guid2.ToString() + "' />          </filter>      </entity>  </fetch>";

                    request = new ExecuteFetchRequest { FetchXml = str36 };
                    response = (ExecuteFetchResponse)servicio.Execute(request);

                    string str37 = this.XmlToMap(response.FetchXmlResult, atributos3)[0][(object)"fib_name"].ToString();
                    str2 = str37.Substring(str37.IndexOf("-") + 1, str37.Length - (str37.IndexOf("-") + 1));
                }
                if (list2[0][(object)"fib_coloniaid3"] != null)
                {
                    guid2 = new Guid(list2[0][(object)"fib_coloniaid3"].ToString());
                    string str36 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_colonia'>          <attribute name='fib_name' />\t\t     <filter type='and'>              <condition attribute='fib_coloniaid' operator='eq' value='" + guid2.ToString() + "' />          </filter>      </entity>  </fetch>";

                    request = new ExecuteFetchRequest { FetchXml = str36 };
                    response = (ExecuteFetchResponse)servicio.Execute(request);

                    string str37 = this.XmlToMap(response.FetchXmlResult, atributos3)[0][(object)"fib_name"].ToString();
                    str3 = str37.Substring(str37.IndexOf("-") + 1, str37.Length - (str37.IndexOf("-") + 1));
                }
                if (list2[0][(object)"fib_formadepagoid"] != null)
                {
                    string str36 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_formadepago'>          <attribute name='fib_codigo' />\t\t     <filter type='and'>              <condition attribute='fib_formadepagoid' operator='eq' value='" + new Guid(list2[0][(object)"fib_formadepagoid"].ToString()).ToString() + "' />          </filter>      </entity>  </fetch>";

                    request = new ExecuteFetchRequest { FetchXml = str36 };
                    response = (ExecuteFetchResponse)servicio.Execute(request);

                    str12 = this.XmlToMap(response.FetchXmlResult, new string[1]
                          {
                            "fib_codigo"
                          })[0][(object)"fib_codigo"].ToString();
                }
                str31 = list2[0][(object)"fib_digitos"] != null ? list2[0][(object)"fib_digitos"].ToString() : "";
                str32 = list2[0][(object)"fib_correoelectronicoadicional"] != null ? list2[0][(object)"fib_correoelectronicoadicional"].ToString() : "";
                str42 = "MX";
            }
            else
            {
                Guid guid1 = new Guid(list1[0][(object)"fib_customerpfid"].ToString());
                string[] atributos2 = new string[25]
                            {
                              "fib_numpersonafisica",
                              "lastname",
                              "fib_apellidomaterno",
                              "firstname",
                              "middlename",
                              "fib_rfc",
                              "fib_curp",
                              "address1_line1",
                              "fib_coloniaid",
                              "address1_postalcode",
                              "fib_address3_line1",
                              "fib_coloniaid3",
                              "fib_address3_postalcode",
                              "fib_address4_line1",
                              "fib_coloniaid4",
                              "fib_address4_postalcode",
                              "telephone2",
                              "mobilephone",
                              "emailaddress1",
                              "fib_impuestosaplicables",
                              "customertypecode",
                              "fib_formadepagoid",
                              "fib_digitos",
                              "fib_correoelectronicoadicional",
                              "fib_paisdenacimientoid"
                            };
                string str33 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='contact'>          <attribute name='fib_numpersonafisica' />          <attribute name='lastname' />          <attribute name='fib_apellidomaterno' />          <attribute name='firstname' />          <attribute name='middlename' />          <attribute name='fib_rfc' />          <attribute name='fib_curp' />          <attribute name='address1_line1' />          <attribute name='fib_coloniaid' />          <attribute name='address1_postalcode' />          <attribute name='fib_address3_line1' />          <attribute name='fib_coloniaid3' />          <attribute name='fib_address3_postalcode' />          <attribute name='fib_address4_line1' />          <attribute name='fib_coloniaid4' />          <attribute name='fib_address4_postalcode' />          <attribute name='telephone2' />          <attribute name='mobilephone' />          <attribute name='emailaddress1' />          <attribute name='fib_impuestosaplicables' />          <attribute name='customertypecode' />          <attribute name='fib_formadepagoid' />          <attribute name='fib_digitos' />          <attribute name='fib_correoelectronicoadicional' />          <attribute name='fib_paisdenacimientoid' />\t\t     <filter type='and'>              <condition attribute='contactid' operator='eq' value='" + guid1.ToString() + "' />          </filter>      </entity>  </fetch>";

                request = new ExecuteFetchRequest { FetchXml = str33 };
                response = (ExecuteFetchResponse)servicio.Execute(request);

                List<Hashtable> list2 = this.XmlToMap(response.FetchXmlResult, atributos2);
                str15 = list2[0][(object)"fib_numpersonafisica"].ToString();
                str16 = (list2[0][(object)"lastname"] != null ? list2[0][(object)"lastname"].ToString() : "") + " " + (list2[0][(object)"fib_apellidomaterno"] != null ? list2[0][(object)"fib_apellidomaterno"].ToString() : "") + " " + (list2[0][(object)"firstname"] != null ? list2[0][(object)"firstname"].ToString() : "") + " " + (list2[0][(object)"middlename"] != null ? list2[0][(object)"middlename"].ToString() : "");
                str8 = list2[0][(object)"firstname"] != null ? list2[0][(object)"firstname"].ToString() : "";
                str9 = list2[0][(object)"middlename"] != null ? list2[0][(object)"middlename"].ToString() : "";
                str10 = list2[0][(object)"lastname"] != null ? list2[0][(object)"lastname"].ToString() : "";
                str11 = list2[0][(object)"fib_apellidomaterno"] != null ? list2[0][(object)"fib_apellidomaterno"].ToString() : "";
                str18 = list2[0][(object)"fib_numpersonafisica"] != null ? list2[0][(object)"fib_numpersonafisica"].ToString() : "";
                str1 = list2[0][(object)"fib_curp"] != null ? list2[0][(object)"fib_curp"].ToString() : "";
                str17 = list2[0][(object)"fib_rfc"] != null ? list2[0][(object)"fib_rfc"].ToString() : "";
                str19 = list2[0][(object)"address1_line1"] != null ? list2[0][(object)"address1_line1"].ToString() : "";
                str20 = list2[0][(object)"address3_line1"] != null ? list2[0][(object)"address3_line1"].ToString() : "";
                str21 = list2[0][(object)"address4_line1"] != null ? list2[0][(object)"address4_line1"].ToString() : "";
                str22 = list2[0][(object)"address1_postalcode"] != null ? list2[0][(object)"address1_postalcode"].ToString() : "";
                str23 = list2[0][(object)"address3_postalcode"] != null ? list2[0][(object)"address3_postalcode"].ToString() : "";
                str24 = list2[0][(object)"address4_postalcode"] != null ? list2[0][(object)"address4_postalcode"].ToString() : "";
                str25 = str16;
                str26 = list2[0][(object)"telephone2"] != null ? list2[0][(object)"telephone2"].ToString() : "";
                str27 = list2[0][(object)"mobilephone"] != null ? list2[0][(object)"mobilephone"].ToString() : "";
                str28 = list2[0][(object)"emailaddress1"] != null ? list2[0][(object)"emailaddress1"].ToString() : "";
                str29 = list2[0][(object)"fib_impuestosaplicables"] != null ? (list2[0][(object)"fib_impuestosaplicables"].ToString() == "4" ? "IVAEXENTO" : "IVAGRAL") : "IVAGRAL";
                switch (int.Parse(list2[0][(object)"customertypecode"].ToString()))
                {
                    case 200000:
                        str4 = "01";
                        break;
                    case 200001:
                        str4 = "02";
                        break;
                    case 200002:
                        str4 = "03";
                        break;
                }
                Guid guid2 = new Guid(list2[0][(object)"fib_coloniaid"].ToString());
                string[] atributos3 = new string[1]
                        {
                          "fib_name"
                        };
                string str34 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_colonia'>          <attribute name='fib_name' />\t\t     <filter type='and'>              <condition attribute='fib_coloniaid' operator='eq' value='" + guid2.ToString() + "' />          </filter>      </entity>  </fetch>";

                request = new ExecuteFetchRequest { FetchXml = str34 };
                response = (ExecuteFetchResponse)servicio.Execute(request);

                string str35 = this.XmlToMap(response.FetchXmlResult, atributos3)[0][(object)"fib_name"].ToString();
                str30 = str35.Substring(str35.IndexOf("-") + 1, str35.Length - (str35.IndexOf("-") + 1));
                if (list2[0][(object)"fib_coloniaid3"] != null)
                {
                    guid2 = new Guid(list2[0][(object)"fib_coloniaid3"].ToString());
                    string str36 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_colonia'>          <attribute name='fib_name' />\t\t     <filter type='and'>              <condition attribute='fib_coloniaid' operator='eq' value='" + guid2.ToString() + "' />          </filter>      </entity>  </fetch>";

                    request = new ExecuteFetchRequest { FetchXml = str36 };
                    response = (ExecuteFetchResponse)servicio.Execute(request);

                    string str37 = this.XmlToMap(response.FetchXmlResult, atributos3)[0][(object)"fib_name"].ToString();
                    str2 = str37.Substring(str37.IndexOf("-") + 1, str37.Length - (str37.IndexOf("-") + 1));
                }
                if (list2[0][(object)"fib_coloniaid4"] != null)
                {
                    guid2 = new Guid(list2[0][(object)"fib_coloniaid4"].ToString());
                    string str36 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_colonia'>          <attribute name='fib_name' />\t\t     <filter type='and'>              <condition attribute='fib_coloniaid' operator='eq' value='" + guid2.ToString() + "' />          </filter>      </entity>  </fetch>";

                    request = new ExecuteFetchRequest { FetchXml = str36 };
                    response = (ExecuteFetchResponse)servicio.Execute(request);

                    string str37 = this.XmlToMap(response.FetchXmlResult, atributos3)[0][(object)"fib_name"].ToString();
                    str3 = str37.Substring(str37.IndexOf("-") + 1, str37.Length - (str37.IndexOf("-") + 1));
                }
                if (list2[0][(object)"fib_formadepagoid"] != null)
                {
                    string str36 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_formadepago'>          <attribute name='fib_codigo' />\t\t     <filter type='and'>              <condition attribute='fib_formadepagoid' operator='eq' value='" + new Guid(list2[0][(object)"fib_formadepagoid"].ToString()).ToString() + "' />          </filter>      </entity>  </fetch>";

                    request = new ExecuteFetchRequest { FetchXml = str36 };
                    response = (ExecuteFetchResponse)servicio.Execute(request);

                    str12 = this.XmlToMap(response.FetchXmlResult, new string[1]
                                  {
                                    "fib_codigo"
                                  })[0][(object)"fib_codigo"].ToString();
                }
                ///JCEn 13/05/2015
                if (list2[0][(object)"fib_paisdenacimientoid"] != null)
                {
                    string strpais = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'><entity name='new_pais'><attribute name='new_codigopais' />\t\t     <filter type='and'>              <condition attribute='new_paisid' operator='eq' value='" + new Guid(list2[0][(object)"fib_paisdenacimientoid"].ToString()).ToString() + "' /></filter> </entity></fetch>";

                    request = new ExecuteFetchRequest { FetchXml = strpais };
                    response = (ExecuteFetchResponse)servicio.Execute(request);

                    str42 = this.XmlToMap(response.FetchXmlResult, new string[1] {"new_codigopais"})[0][(object)"new_codigopais"].ToString();
                }
                ///JCEN

                str31 = list2[0][(object)"fib_digitos"] != null ? list2[0][(object)"fib_digitos"].ToString() : "";
                str32 = list2[0][(object)"fib_correoelectronicoadicional"] != null ? list2[0][(object)"fib_correoelectronicoadicional"].ToString() : "";
            }
            string str38 = list1[0][(object)"fib_name"].ToString();
            double num1 = double.Parse(list1[0][(object)"fib_diferenciaimporte"].ToString());
            if (list1[0][(object)"fib_valorgarantiaprincipal"] != null)
                double.Parse(list1[0][(object)"fib_valorgarantiaprincipal"].ToString());
            Guid guid = new Guid(list1[0][(object)"fib_monedaid"].ToString());
            string[] atributos4 = new string[1]
                          {
                            "fib_codigodemoneda"
                          };
            string str39 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_catalogodemoneda'>          <attribute name='fib_codigodemoneda' />\t\t     <filter type='and'>              <condition attribute='fib_catalogodemonedaid' operator='eq' value='" + guid.ToString() + "' />          </filter>      </entity>  </fetch>";

            request = new ExecuteFetchRequest { FetchXml = str39 };
            response = (ExecuteFetchResponse)servicio.Execute(request);

            string str40 = this.XmlToMap(response.FetchXmlResult, atributos4)[0][(object)"fib_codigodemoneda"].ToString();

            string[] atributos5 = new string[5]
                      {
                        "fib_garantasid",
                        "fib_valorgarantia",
                        "fib_name",
                        "fib_monedaid",
                        "statuscode"
                      };
            string str41 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_garantiadelineadecredito'>          <attribute name='fib_garantasid' />          <attribute name='fib_valorgarantia' />          <attribute name='fib_name' />          <attribute name='fib_monedaid' />          <attribute name='statuscode' />\t\t     <filter type='and'>              <condition attribute='fib_garantasid' operator='eq' value='" + idLineaCredito.ToString() + "' />          </filter>      </entity>  </fetch>";

            request = new ExecuteFetchRequest { FetchXml = str41 };
            response = (ExecuteFetchResponse)servicio.Execute(request);

            List<Hashtable> list3 = this.XmlToMap(response.FetchXmlResult, atributos5);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.RemoveAll();
            XmlNode newChild1 = (XmlNode)xmlDocument.CreateElement("garantias");
            xmlDocument.AppendChild(newChild1);
            int num2 = 0;

            if (list3 != null)
            {
                foreach (Hashtable hashtable1 in list3)
                {
                    Hashtable hashtable2 = hashtable1;
                    if (int.Parse(hashtable2[(object)"statuscode"].ToString()) == 1)
                    {
                        string[] atributos2 = new string[1]
                                {
                                  "fib_codigodemoneda"
                                };
                        string str33 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_catalogodemoneda'>          <attribute name='fib_codigodemoneda' />\t\t     <filter type='and'>              <condition attribute='fib_catalogodemonedaid' operator='eq' value='" + hashtable1[(object)"fib_monedaid"].ToString() + "' />          </filter>      </entity>  </fetch>";

                        request = new ExecuteFetchRequest { FetchXml = str33 };
                        response = (ExecuteFetchResponse)servicio.Execute(request);

                        List<Hashtable> list2 = this.XmlToMap(response.FetchXmlResult, atributos2);
                        XmlNode newChild2 = (XmlNode)xmlDocument.CreateElement("garantia");
                        XmlAttribute attribute1 = xmlDocument.CreateAttribute("descrip");
                        attribute1.InnerText = hashtable2[(object)"fib_name"].ToString();
                        newChild2.Attributes.Append(attribute1);
                        XmlAttribute attribute2 = xmlDocument.CreateAttribute("importe");
                        attribute2.InnerText = hashtable2[(object)"fib_valorgarantia"].ToString();
                        newChild2.Attributes.Append(attribute2);
                        XmlAttribute attribute3 = xmlDocument.CreateAttribute("moneda");
                        attribute3.InnerText = list2[0][(object)"fib_codigodemoneda"].ToString();
                        newChild2.Attributes.Append(attribute3);
                        newChild1.AppendChild(newChild2);
                        ++num2;
                    }
                }
            }
            string xmlCal = num2 > 0 ? ((object)xmlDocument.OuterXml).ToString() : "";
            string line;
            bool flag;
            try
            {
                AxServiceProd axServiceProd = new AxServiceProd();
                string xmlSerializado1 = this.SerializarToXml(new object[34]
                    {
                      (object) str18,
                      (object) str16,
                      (object) str16,
                      (object) "MXN",
                      (object) "",
                      (object) "",
                      (object) str29,
                      (object) str17,
                      (object) str1,
                      (object) str5,
                      (object) str19,
                      (object) str30.Trim(),
                      (object) str22,
                      (object) str6,
                      (object) str20,
                      (object) str2.Trim(),
                      (object) str23,
                      (object) str7,
                      (object) str21,
                      (object) str3.Trim(),
                      (object) str24,
                      (object) str25,
                      (object) str26,
                      (object) str27,
                      (object) str28,
                      (object) str4,
                      (object) str8,
                      (object) str9,
                      (object) str10,
                      (object) str11,
                      (object) str12,
                      (object) str31,
                      (object) str32,
                      (object) str42
                    });


                //Arrendadora

                
                Tuple<InfoConexion, Uri> infoConexion1 = this.getInfoConexion(servicio, 1);
                InfoConexion first1 = infoConexion1.First;
                axServiceProd.Url = infoConexion1.Second.ToString();
                line = axServiceProd.altaCliente(this.DomainLogonName, xmlSerializado1, first1);

                Tuple<InfoConexion, Uri> infoConexion2 = this.getInfoConexion(servicio, 2);
                InfoConexion first2 = infoConexion2.First;
                axServiceProd.Url = infoConexion2.Second.ToString();
                line = axServiceProd.altaCliente(this.DomainLogonName, xmlSerializado1, first2);

                Tuple<InfoConexion, Uri> infoConexion4 = this.getInfoConexion(servicio, 3);
                InfoConexion first4 = infoConexion4.First;
                axServiceProd.Url = infoConexion4.Second.ToString();
                line = axServiceProd.altaCliente(this.DomainLogonName, xmlSerializado1, first4);
                             
                int tipoProducto = this.getTipoProducto(servicio, idLineaCredito);

                Tuple<InfoConexion, Uri> infoConexion3 = this.getInfoConexion(servicio, tipoProducto);
                InfoConexion first3 = infoConexion3.First;
                axServiceProd.Url = infoConexion3.Second.ToString();
                if (line.Contains("Exito"))
                {
                    ///Llamar a Alta empleador

                    string xmlSerializado2 = this.SerializarToXml(new object[4]
                              {
                                (object) str38,
                                (object) str15,
                                (object) num1,
                                (object) str40
                              });
                    line = axServiceProd.altaLinea(this.DomainLogonName, xmlSerializado2, first3);
                    if (line.Contains("Exito"))
                    {
                        string xmlSerializado3 = this.SerializarToXml(new object[11]
                                {
                                  (object) "DISPLINEACREDITO",
                                  (object) num1,
                                  (object) 0,
                                  (object) str40,
                                  (object) "",
                                  (object) "",
                                  (object) "",
                                  (object) "",
                                  (object) "",
                                  (object) "",
                                  (object) (" LC: " + list1[0][(object) "fib_name"].ToString())
                                });

                        line = axServiceProd.contabilizaEvento(this.DomainLogonName, xmlSerializado3, first3);
                        if (line.Contains("Exito"))
                        {
                            string xmlSerializado4 = this.SerializarToXml(new object[1]
              {
                (object) str38
              });
                            line = axServiceProd.altaGarantias(this.DomainLogonName, xmlSerializado4, xmlCal, first3);
                            flag = line.Contains("Exito");


                        }
                    }
                }
                if (line.Contains("Error"))
                {
                    Entity entity = new Entity("fib_lineadecredito");
                    entity.Id = new Guid(list1[0][(object)"fib_lineadecreditoid"].ToString());

                    entity.Attributes.Add("fib_estatus", new OptionSetValue(1));

                    servicio.Update(entity);
                }
                
            }
            catch (SoapException ex)
            {
                throw new InvalidPluginExecutionException("Error: " + ex.Message, (Exception)ex);
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Error: " + ex.Message);
            }
            flag = line.Contains("Error");
            if (line.Contains("Error"))
                throw new InvalidPluginExecutionException(line);
        }

        private string SerializarToXml(object[] myparams)
        {
            try
            {
                StringWriter stringWriter = new StringWriter();
                new XmlSerializer(myparams.GetType()).Serialize((TextWriter)stringWriter, (object)myparams);
                string str = stringWriter.ToString();
                stringWriter.Close();
                return str;
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Error: " + ex.Message);
            }
        }

        private void setDomainLogonName(IOrganizationService servicio, IPluginExecutionContext context)
        {
            try
            {
                this.DomainLogonName = servicio.Retrieve("systemuser", context.UserId, new ColumnSet("domainname"))["domainname"].ToString();
            }
            catch (SoapException ex)
            {
                throw new InvalidPluginExecutionException("Error: " + ex.Message, (Exception)ex);
            }
        }

        public List<Hashtable> XmlToMap(string resultXml, string[] atributos)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(resultXml);
                XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("result");
                if (xmlNodeList == null || xmlNodeList.Count == 0)
                    return (List<Hashtable>)null;
                List<Hashtable> list = new List<Hashtable>();
                foreach (XmlNode xmlNode1 in xmlNodeList)
                {
                    Hashtable hashtable = new Hashtable();
                    foreach (string xpath in atributos)
                    {
                        XmlNode xmlNode2 = xmlNode1.SelectSingleNode(xpath);
                        hashtable.Add((object)xpath, xmlNode2 != null ? (object)xmlNode2.InnerText : (object)(string)null);
                    }
                    list.Add(hashtable);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("[BasePlugin.XmlToMap()] Error: " + ex.Message, ex.InnerException);
            }
        }

        private int getTipoProducto(IOrganizationService servicio, Guid LineaCredito)
        {
            string[] atributos = new string[2]
                  {
                    "fib_lineadecreditoid",
                    "fib_esquemaid.fib_tipodeproducto"
                  };
            string str = "";//"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'><entity name='fib_lineadecredito'><attribute name='fib_lineadecreditoid'/><order attribute='createdon' descending='false'/><filter type='and'><condition attribute='fib_lineadecreditoid' operator='eq' value='" + LineaCredito.ToString() + "'/></filter><link-entity name='fib_disponibleporproducto' from='fib_lineadecreditoid' to='fib_lineadecreditoid'><link-entity name='fib_producto' from='fib_productoid' to='fib_productoid'><link-entity name='fib_esquema' from='fib_esquemaid' to='fib_esquemaid'><attribute name='fib_tipodeproducto' alias='tipodeproducto'/></link-entity></link-entity></link-entity></entity></fetch>";
            str += @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
	                    <entity name='fib_lineadecredito'>
		                    <attribute name='fib_lineadecreditoid'/>
		                    <order attribute='createdon' descending='false'/>
		                    <filter type='and'>
			                    <condition attribute='fib_lineadecreditoid' operator='eq' value='" + LineaCredito.ToString() + @"'/>
		                    </filter>
		                    <link-entity name='fib_disponibleporproducto' from='fib_lineadecreditoid' to='fib_lineadecreditoid'>
			                    <link-entity name='fib_producto' from='fib_productoid' to='fib_productoid'>
				                    <link-entity name='fib_esquema' from='fib_esquemaid' to='fib_esquemaid' alias='aa'>
					                    <attribute name='fib_tipodeproducto'/>
				                    </link-entity>
			                    </link-entity>
		                    </link-entity>
	                    </entity>
                    </fetch>";
            EntityCollection entidades = servicio.RetrieveMultiple(new FetchExpression(str));

            if (entidades[0].Attributes.Contains("aa.fib_tipodeproducto"))
            {
                var option = ((AliasedValue)entidades[0]["aa.fib_tipodeproducto"]).Value as OptionSetValue;
                return int.Parse(option.Value.ToString());
            }
            else
            {
                throw new Exception("ASIGNE EL TIPO DE PRODUCTO AL ESQUEMA DE LOS PRODUCTOS DE ESTE CLIENTE.");
            }

            //var request = new ExecuteFetchRequest { FetchXml = str };
            //var response = (ExecuteFetchResponse)servicio.Execute(request);

            //List<Hashtable> list = this.XmlToMap(response.FetchXmlResult, atributos);
            //if (list[0][(object)"fib_esquemaid.fib_tipodeproducto"] != null)
            //{
            //    int num = int.Parse(list[0][(object)"fib_esquemaid.fib_tipodeproducto"].ToString());
            //    return num;
            //}
            //else
            //{
            //    throw new Exception("ASIGNE EL TIPO DE PRODUCTO AL ESQUEMA DE LOS PRODUCTOS DE ESTE CLIENTE.");
            //}
        }

        private Tuple<InfoConexion, Uri> getInfoConexion(IOrganizationService servicio, int tipoCliente)
        {
            InfoConexion first = new InfoConexion();
            string[] atributos;
            string str1;

            switch (tipoCliente)
            {
                case 1:
                    atributos = new string[5]
                    {
                      "fib_fil_usuarioax",
                      "fib_fil_dominiousrax",
                      "fib_fil_companiaax",
                      "fib_fil_servidorax",
                      "fib_fil_urlwebserviceax"
                    };
                    str1 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_configuracion'>          <attribute name='fib_fil_usuarioax'/>          <attribute name='fib_fil_dominiousrax'/>          <attribute name='fib_fil_companiaax'/>          <attribute name='fib_fil_servidorax'/>          <attribute name='fib_fil_urlwebserviceax'/>      </entity>  </fetch>";
                    break;
                case 2:
                     atributos = new string[5]
                            {
                              "fib_ter_usuarioax",
                              "fib_ter_dominiousrax",
                              "fib_ter_companiaax",
                              "fib_ter_servidorax",
                              "fib_ter_urlwebserviceax"
                            };
                    str1 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_configuracion'>          <attribute name='fib_ter_usuarioax'/>          <attribute name='fib_ter_dominiousrax'/>          <attribute name='fib_ter_companiaax'/>          <attribute name='fib_ter_servidorax'/>          <attribute name='fib_ter_urlwebserviceax'/>      </entity>  </fetch>";
                    break;
                default:
                     atributos = new string[5]
                            {
                              "fib_arr_usuarioax",
                              "fib_arr_dominiousrax",
                              "fib_arr_companiaax",
                              "fib_arr_servidorax",
                              "fib_arr_urlwebserviceax"
                            };
                    str1 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_configuracion'>          <attribute name='fib_arr_usuarioax'/>          <attribute name='fib_arr_dominiousrax'/>          <attribute name='fib_arr_companiaax'/>          <attribute name='fib_arr_servidorax'/>          <attribute name='fib_arr_urlwebserviceax'/>      </entity>  </fetch>";
                    break;

            }
            /*
            if (tipoCliente == 1)
            {
                atributos = new string[5]
                    {
                      "fib_fil_usuarioax",
                      "fib_fil_dominiousrax",
                      "fib_fil_companiaax",
                      "fib_fil_servidorax",
                      "fib_fil_urlwebserviceax"
                    };
                str1 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_configuracion'>          <attribute name='fib_fil_usuarioax'/>          <attribute name='fib_fil_dominiousrax'/>          <attribute name='fib_fil_companiaax'/>          <attribute name='fib_fil_servidorax'/>          <attribute name='fib_fil_urlwebserviceax'/>      </entity>  </fetch>";
            }
            else
            {
                atributos = new string[5]
                            {
                              "fib_ter_usuarioax",
                              "fib_ter_dominiousrax",
                              "fib_ter_companiaax",
                              "fib_ter_servidorax",
                              "fib_ter_urlwebserviceax"
                            };
                str1 = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>       <entity name='fib_configuracion'>          <attribute name='fib_ter_usuarioax'/>          <attribute name='fib_ter_dominiousrax'/>          <attribute name='fib_ter_companiaax'/>          <attribute name='fib_ter_servidorax'/>          <attribute name='fib_ter_urlwebserviceax'/>      </entity>  </fetch>";
            }
            */
            var request = new ExecuteFetchRequest { FetchXml = str1 };
            var response = (ExecuteFetchResponse)servicio.Execute(request);

            List<Hashtable> list = this.XmlToMap(response.FetchXmlResult, atributos);
            Uri second = (Uri)null;
            if (list != null)
            {
                string[] strArray = this.DomainLogonName.Split(new char[1]
                        {
                          '\\'
                        });
                string str2 = strArray[0];
                first.usuario = strArray[1];
                first.dominio = str2.ToLower().Trim() == "bepensa" ? str2.ToLower() + ".local" : str2.ToLower() + ".bepensa.local";

                switch (tipoCliente)
                {
                    case 1:
                        first.empresa = list[0][(object)"fib_fil_companiaax"].ToString();
                        first.servidor = list[0][(object)"fib_fil_servidorax"].ToString();
                        second = new Uri(list[0][(object)"fib_fil_urlwebserviceax"].ToString());
                        break;
                    case 2:
                        first.empresa = list[0][(object)"fib_ter_companiaax"].ToString();
                        first.servidor = list[0][(object)"fib_ter_servidorax"].ToString();
                        second = new Uri(list[0][(object)"fib_ter_urlwebserviceax"].ToString());
                        break;
                    default:
                        first.empresa = list[0][(object)"fib_arr_companiaax"].ToString();
                        first.servidor = list[0][(object)"fib_arr_servidorax"].ToString();
                        second = new Uri(list[0][(object)"fib_arr_urlwebserviceax"].ToString());
                        break;

                }
                /*
                if (tipoCliente == 1)
                {
                    first.empresa = list[0][(object)"fib_fil_companiaax"].ToString();
                    first.servidor = list[0][(object)"fib_fil_servidorax"].ToString();
                    second = new Uri(list[0][(object)"fib_fil_urlwebserviceax"].ToString());
                }
                else
                {
                    first.empresa = list[0][(object)"fib_ter_companiaax"].ToString();
                    first.servidor = list[0][(object)"fib_ter_servidorax"].ToString();
                    second = new Uri(list[0][(object)"fib_ter_urlwebserviceax"].ToString());
                }*/
            }
            return new Tuple<InfoConexion, Uri>(first, second);
        }
    }
}