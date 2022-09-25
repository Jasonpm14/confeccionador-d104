using Confeccionador_D_104.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Automation.Peers;
using System.Xml;

namespace Confeccionador_D_104.Helpers
{
    public  class XmlReader
    {
        private string personId; //Id of declarator for comparison with read invoices

        private DeclarationConfigDataModel currentDeclarationConfigData;

        public XmlReader(string personId, int year, int month)
        {
            this.personId = personId;

            this.currentDeclarationConfigData = new DeclarationConfigDataModel(personId, month, year);
        }

        public void FindInvoices(DirectoryInfo root)
        {
            FileInfo[] files = null;

            DirectoryInfo[] subDirs = null;

            files = root.GetFiles("*.xml");

            if (files != null)
            {
                foreach (FileInfo file in files)
                {

                   LoadXmlDocument(file.FullName);

                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    FindInvoices(dirInfo);
                }
            }

        }

        private void LoadXmlDocument(string xmlPath)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(xmlPath);

            if (doc.DocumentElement.Name == "FacturaElectronica" ||
                doc.DocumentElement.Name == "NotaCreditoElectronica")
            {
                DateTime documentDate = Convert.ToDateTime(doc.ChildNodes[1].ChildNodes[3].InnerText);

                if(currentDeclarationConfigData.IsDateInCurrentPeriod(
                    new DateTime(documentDate.Year, documentDate.Month, documentDate.Day)))
                {
                    Invoice newInvoice = ReadXmlNodes(doc);

                    if (newInvoice.isForCurrentDeclarator(personId))
                        GlobalData.INCOME_INVOICES.Add(newInvoice);
                    else
                        GlobalData.EXPENSE_INVOICES.Add(newInvoice);
                }
            }
        }

        private Invoice ReadXmlNodes(XmlDocument doc)
        {
            Invoice xmlToInvoice = new Invoice();

            foreach (XmlNode node in doc.ChildNodes[1])
            {
                switch (node.Name)
                {
                    case "NumeroConsecutivo":
                        xmlToInvoice.InvoiceNumber= node.InnerText;
                        break;

                    case "FechaEmision":


                        break;

                    case "Emisor":

                        Invoice emisorInfo = ReadEmisorAndReceptorNodes(node);
                        xmlToInvoice.EmisorId = emisorInfo.EmisorId;
                        xmlToInvoice.EmisorName = emisorInfo.EmisorName;
                        xmlToInvoice.EmisorEmail = emisorInfo.EmisorEmail;

                        break;

                    case "Receptor":

                        /*
                            Using same function since Emisor and Receptor xml structure is the same
                            Note that since return object has the info in emisor variables... I am saving
                            the values in the corresponding customer variables
                        */

                        Invoice receptorInfo = ReadEmisorAndReceptorNodes(node);

                        xmlToInvoice.CustomerId = receptorInfo.EmisorId;
                        xmlToInvoice.CustomerName = receptorInfo.EmisorName;
                        xmlToInvoice.CustomerEmail = receptorInfo.EmisorEmail;

                        break;

                    case "DetalleServicio":

                        xmlToInvoice.DetailLines = ReadInvoiceLinesNodes(node);

                        break;

                    default:
                        break;
                }
            }//End foreach

            return xmlToInvoice;

        }//End of Method ReadXmlNodes

        private Invoice ReadEmisorAndReceptorNodes(XmlNode infoNode)
        {
            Invoice InvoiceEmisor_ReceptorInfo = new Invoice();

            foreach (XmlNode node in infoNode)
            {
                switch (node.Name)
                {

                    case "Nombre":
                        InvoiceEmisor_ReceptorInfo.EmisorName = node.InnerText;
                        break;

                    case "Identificacion":
                        foreach (XmlNode idNode in node)
                        {
                            if (idNode.Name == "Numero")
                                InvoiceEmisor_ReceptorInfo.EmisorId = idNode.InnerText;
                        }
                        break;

                    case "CorreoElectronico":
                        InvoiceEmisor_ReceptorInfo.EmisorEmail = node.InnerText;
                        break;

                    default:
                        break;
                }
            }
            return InvoiceEmisor_ReceptorInfo;
        }

        private List<InvoiceDetail> ReadInvoiceLinesNodes(XmlNode detailsNode)
        {
            Invoice invoiceDetails = new Invoice();

            foreach (XmlNode linea in detailsNode)
            {
                InvoiceDetail detailLine = new InvoiceDetail();

                foreach (XmlNode lineNode in linea)
                {
                    switch (lineNode.Name)
                    {

                        case "Codigo":

                            detailLine.Code = lineNode.InnerText;

                            //Collecting each cabys code in global variable to make a future unique rest request to atv api
                            if (!GlobalData.CABYS_CODES_LIST.Contains(detailLine.Code))
                                GlobalData.CABYS_CODES_LIST.Add(detailLine.Code);

                            break;

                        case "Detalle":

                            detailLine.Description = lineNode.InnerText;
                            break;

                        case "SubTotal":

                            detailLine.Subtotal = float.Parse(lineNode.InnerText);
                            break;

                        case "Impuesto":

                            foreach (XmlNode taxNode in lineNode)
                            {
                                switch (taxNode.Name)
                                {
                                    case "Tarifa":
                                        detailLine.TaxRate = float.Parse(taxNode.InnerText);
                                        break;

                                    case "Monto":
                                        detailLine.TaxTotal = float.Parse(taxNode.InnerText);
                                        break;

                                    default:
                                        break;
                                }
                            }

                            break;

                        default:
                            break;
                    }//End Switch
                }//End 2nd foreach

                invoiceDetails.DetailLines.Add(detailLine);

            }//End foreach

            return invoiceDetails.DetailLines;

        }//End of Method ReadInvoiceLinesNodes
    }
}
