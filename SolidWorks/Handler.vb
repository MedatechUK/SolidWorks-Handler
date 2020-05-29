Imports System.ComponentModel.Composition
Imports System.Xml
Imports Newtonsoft.Json
Imports System.Web
Imports System.IO
Imports System.Reflection
Imports MedatechUK.Web
Imports MedatechUK.oData
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
            For Each D As document In .document
                recurse(D)
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

    Private Sub recurse(ByRef d As document)

        If Not d.configuration.references Is Nothing Then
            For Each sd As document In d.configuration.references
                recurse(sd)
            Next
        End If

        Using F As New Loading("SW", AddressOf logHandler)
            With F
                With .AddRow(1)
                    '.REVNAME = d.configuration.revision
                    '.PARTNAME = d.configuration.number
                    '.PARTDES = d.configuration.description
                    '.SPEC1 = d.configuration.finish
                    '.SPEC2 = d.configuration.material
                    '.SPEC3 = d.configuration.weight
                    '.SPEC4 = d.configuration.process_4
                    '.SPEC5 = d.configuration.process_1
                    '.SPEC6 = d.configuration.process_2
                    '.SPEC7 = d.configuration.process_3
                    '.SPEC8 = d.configuration.product_type
                    '.SPEC9 = d.configuration.supplier

                    .TEXT1 = d.configuration.revision
                    .TEXT2 = d.configuration.number
                    .TEXT3 = d.configuration.description
                    .TEXT4 = d.configuration.finish
                    .TEXT5 = d.configuration.material
                    .TEXT6 = d.configuration.weight
                    .TEXT7 = d.configuration.process_4
                    .TEXT8 = d.configuration.process_1
                    .TEXT9 = d.configuration.process_2
                    .TEXT10 = d.configuration.process_3
                    .TEXT11 = d.configuration.product_type
                    .TEXT12 = d.configuration.supplier

                End With

                If Not d.configuration.references Is Nothing Then
                    For Each s In d.configuration.references
                        With .AddRow(2)
                            '.ACTNAME = "Issue"
                            '.SONACTNAME = "Issue"
                            .TEXT13 = s.configuration.number
                            .REAL1 = s.configuration.Reference_Count
                            '.SONREVNAME = s.configuration.revision

                        End With

                    Next
                End If

                .Post()

            End With

        End Using

    End Sub

    ''' <summary>
    ''' Handles oData logging.
    ''' oData events are fired into this method by instantiating the loading with 
    ''' an event handler.
    ''' </summary>
    ''' <param name="sender">The calling object</param>
    ''' <param name="e">The log arguments</param>
    Private Sub logHandler(sender As Object, e As LogArgs)
        log.LogData.AppendFormat("{0}", e.Message).AppendLine()
        If e.isException Then
            log.EntryType = LogEntryType.FailureAudit

        End If

    End Sub


End Class