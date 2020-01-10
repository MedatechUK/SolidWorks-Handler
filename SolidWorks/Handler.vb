Imports System.ComponentModel.Composition
Imports System.Xml
Imports Newtonsoft.Json
Imports System.Web
Imports System.IO
Imports System.Reflection
Imports PriPROC6.Interface.Web
Imports System.Xml.Serialization

<Export(GetType(xmlHandler))>
<ExportMetadata("EndPoint", "SolidWorks")>
<ExportMetadata("Hidden", False)>
Public Class SolidWorks : Inherits iHandler : Implements xmlHandler

    ''' <summary>
    ''' Overides the XML schema for the handler
    ''' </summary>
    ''' <param name="Schemas">The XmlSchemaSet for the request</param>
    Public Overrides Sub XmlStylesheet(ByRef Schemas As Schema.XmlSchemaSet)
        With Schemas
            .Add(
                "SolidWorks_1.0",
                New XmlTextReader(
                    New StringReader(
                        My.Resources.SolidWorks
                    )
                )
            )
        End With

    End Sub

    ''' <summary>
    ''' Overrides XML handler w ith a StreamReader for business object parsing.
    ''' </summary>
    ''' <param name="w"></param>
    ''' <param name="Request"></param>
    Public Overrides Sub StreamHandler(ByRef w As XmlTextWriter, ByRef Request As StreamReader)

        Dim s As New XmlSerializer(GetType(xml))
        Dim o As xml = s.Deserialize(Request)

        With o.transactions.transaction
            CheckPart(.document)
            For Each document In .document.configuration.references
                CheckPart(document)

            Next

        End With

        With w
            .WriteStartElement("response")
            .WriteAttributeString("status", CStr(200))
            .WriteAttributeString("bubbleid", BubbleID)
            .WriteAttributeString("message", "OK")
            .WriteEndElement() 'End Settings 

        End With

    End Sub

    Private Sub CheckPart(ByRef doc As xmlTransactionsTransactionDocument)

        Debug.Print(doc.pdmweid)

        Using F As New PriBase.PART(
            Assembly.Load(
                "PriBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            )
        )
            With F
                With .AddRow()
                    For Each a In doc.configuration.attribute
                        Select Case a.name.ToLower
                            Case "document pdmweid"
                            Case "originationdate"
                            Case "revision"
                            Case "number"
                                .PARTNAME = a.value
                            Case "description"
                                .PARTDES = a.value
                            Case "project"
                            Case "material"
                            Case "finish"
                            Case "author"
                            Case "weight"
                            Case "process 4"
                            Case "process 1"
                            Case "process 2"
                            Case "process 3"
                            Case "cross reference"
                            Case "product type"
                            Case "product used with"
                            Case "supplier"
                            Case "bend radius"


                        End Select
                    Next

                    If Not .Get() Then .Post()

                End With

            End With

        End Using

    End Sub

    '''' <summary>
    '''' Overrides XML handler with an XML document for manual parsing.
    '''' </summary>
    '''' <param name="w">The response stream as a XmlTextWriter</param>
    '''' <param name="Request">The request as an XmlDocument</param>
    'Public Overrides Sub XMLHandler(ByRef w As XmlTextWriter, ByRef Request As XmlDocument)

    '    'log.LogData.AppendFormat("Running in company {0}.", requestEnv).AppendLine()

    '    'Using F As New CUSTOMERS(Assembly.GetExecutingAssembly)
    '    '    With F
    '    '        With .AddRow()
    '    '            .CUSTNAME = "TQ000043"
    '    '            .BUSINESSTYPE = "BusType"
    '    '            .OWNERLOGIN = "SimonB"
    '    '            .CREATEDDATE = Now

    '    '            If Not .Get() Then Throw .Exception

    '    '            With .CUSTPERSONNEL.AddRow
    '    '                .NAME = "Joe Bloggs"
    '    '                .AGENTCODE = "007"
    '    '                .CIVFLAG = "N"

    '    '                If .Post Then

    '    '                End If

    '    '            End With

    '    '            .ADDRESS3 = "test"
    '    '            If .Patch() Then

    '    '            End If

    '    '        End With

    '    '    End With

    '    'End Using

    'End Sub

End Class