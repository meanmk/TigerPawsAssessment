<?xml version="1.0"?> 
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

	<xsl:template match="Products">
		<html>
        <head>
			<title>Our Products</title>
			<style>
        body {  "Neucha", -apple-system, system-ui, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif }
        div { background-color:#FFD57E; margin-top:20px; padding:7px 7px 7px 7px}
        h1 { margin: 3%; width: 94%; padding: 10px 10px 10px 20px;  border-radius: 10px; font-size:120%}
        table {background-color:#FFD57E; ; margin: 3%; width: 94% }
        th , td {padding: 8px}
        p { padding-left: 20px}
      </style>
		</head>
        <body>
        <h1>Tiger Paws Products</h1>
        <table border="1" cellpadding="5" >
         <tr bgcolor="#F0978B" >
              <th>Id</th>
              <th>Name</th>
              <th> Genre</th>
              <th>Description</th>
              <th>Number in Stock</th>             
         </tr>
        <xsl:for-each select="Product">
         <tr>
            <td>
                <xsl:value-of select="Id"/>
            </td>
            <td>  
                <xsl:value-of select="Name"/>
            </td>
            <td>
            
                <xsl:value-of select="Genre"/>
            </td>
            <td>
            
                <xsl:value-of select="Description"/>
            </td>
            <td> 
                
                <xsl:value-of select="NumberInStock"/>
            </td>   
          </tr>     
        </xsl:for-each>
         </table>
        </body>
		</html>
	</xsl:template>

</xsl:stylesheet>