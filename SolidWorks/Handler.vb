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
    ''' Overrides XML handler with a StreamReader for business object parsing.
    ''' </summary>
    ''' <param name="w"></param>
    ''' <param name="Request"></param>
    Public Overrides Sub StreamHandler(ByRef w As XmlTextWriter, ByRef Request As StreamReader)

        Dim s As New XmlSerializer(GetType(xml))
        Dim o As xml = s.Deserialize(Request)

        With o.transactions.transaction.document
            Debug.Print(.pdmweid)

            With .configuration
                For Each a In .attribute
                    Select Case a.name.ToLower
                        Case ""
                            Debug.Print(a.value)

                    End Select
                Next

                For Each sublevel In .references
                    With sublevel
                        Debug.Print(.pdmweid)

                        With .configuration
                            For Each a In .attribute
                                Select Case a.name.ToLower
                                    Case ""
                                        Debug.Print(a.value)

                                End Select
                            Next
                        End With

                    End With

                Next

            End With

        End With

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