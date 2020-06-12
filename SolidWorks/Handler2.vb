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
<ExportMetadata("EndPoint", "SolidWorks2")>
<ExportMetadata("Hidden", False)>
Public Class SolidWorks2 : Inherits iHandler : Implements xmlHandler

    ''' <summary>
    ''' Overides the XML schema for the handler
    ''' </summary>
    ''' <param name="Schemas">The XmlSchemaSet for the request</param>
    Public Overrides Sub XmlStylesheet(ByRef Schemas As Schema.XmlSchemaSet)
        With Schemas
            .Add(
                "SolidWorks_2.0",
                New XmlTextReader(
                    New StringReader(
                        My.Resources.SolidWorks2
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

        Dim s As New XmlSerializer(GetType(Root))
        Dim o As Root = s.Deserialize(Request)

        For Each r As RootRow In o.Row
            Using F As New Loading("SW", AddressOf logHandler)
                With F
                    With .AddRow(1)
                        If r.INTERNAL_CODE.Length = 0 Then
                            .TEXT2 = r.PART_NUMBER
                        Else
                            .TEXT2 = r.INTERNAL_CODE
                            .TEXT4 = r.PART_NUMBER
                        End If

                        If Len(r.DESCRIPTION) = 0 Then
                            .TEXT3 = r.PART_NUMBER
                        Else
                            .TEXT3 = Left(r.DESCRIPTION, 48)
                        End If

                        .TEXT12 = r.MANUFACTURER

                    End With

                    .Post()

                End With

            End Using

        Next

        Using F As New Loading("SW", AddressOf logHandler)
            With F
                With .AddRow(1)
                    .TEXT2 = Split(HttpContext.Current.Request.Headers("POST_FILE"), ".")(0)
                    .TEXT3 = Split(HttpContext.Current.Request.Headers("POST_FILE"), ".")(0)

                End With

                For Each r As RootRow In o.Row
                    With .AddRow(2)
                        '.ACTNAME = "Issue"
                        '.SONACTNAME = "Issue"
                        .TEXT13 = r.PART_NUMBER
                        .REAL1 = r.QUANTITY
                        '.SONREVNAME = s.configuration.revision

                    End With

                Next

                .Post()

            End With

        End Using

        With w
            .WriteStartElement("response")
            .WriteAttributeString("status", CStr(200))
            .WriteAttributeString("bubbleid", BubbleID)
            .WriteAttributeString("message", "OK")
            .WriteEndElement() 'End Settings 

        End With

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