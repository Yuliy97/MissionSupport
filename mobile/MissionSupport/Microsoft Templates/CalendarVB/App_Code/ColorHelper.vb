Imports System.Drawing

Public NotInheritable Class ColorHelper

    ''' <summary>
    ''' Get a random hex color
    ''' </summary>
    ''' <return>Random hex color</return>
    Private Sub New()
    End Sub
    Public Shared Function GetRandomColor() As String
        Dim random = New Random
        Dim r = random.Next(50, 255)
        Dim g = random.Next(50, 255)
        Dim b = random.Next(50, 255)

        Return ColorTranslator.ToHtml(Color.FromArgb(r, g, b))
    End Function

    ''' <summary>
    ''' Checks whether an html hex color isn't too dark
    ''' </summary>
    ''' <param name="htmlColor">The hex color to check</param>
    ''' <returns>true if the color is bright enough, false otherwise</returns>
    Public Shared Function IsValidColor(ByVal htmlColor As String) As Boolean
        ' Bright enough means that the
        ' color is valid and it's r+g+b are greater than 150

        Try
            Dim c = ColorTranslator.FromHtml(htmlColor)
            Return ((CInt(c.R) + CInt(c.G) + CInt(c.B)) >= 150)
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Returns a darker color for use as a border
    ''' </summary>
    ''' <param name="hex">The color to make darker</param>
    ''' <returns>A darker hex color than the color passed</returns>
    Public Shared Function GetBorderFromHtml(ByVal hex As String) As String

        ' I want this to throw an exception on invalid
        ' hex values. It should only be used from the
        ' developers side, so that should be safe.

        Dim color = ColorTranslator.FromHtml(hex)

        ' I should convert to HSV or HSL and then lower V/L
        ' These methods aren't in .net. Instead i'm lowering each
        ' R,G, and B value

        Dim newR = CInt(color.R * 0.5)
        Dim newG = CInt(color.G * 0.5)
        Dim newB = CInt(color.B * 0.5)

        Dim darker = color.FromArgb(newR, newG, newB)
        Return ColorTranslator.ToHtml(darker)
    End Function
End Class