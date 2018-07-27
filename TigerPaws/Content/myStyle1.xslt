<?xml version="1.0"?> 
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

	<xsl:template match="DocumentElement">
		<html>
        <head>
			<title>Our Products</title>
			<style>
        body {"Neucha", -apple-system, system-ui, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif}
        div { background-color:#FFD57E;  padding:7px 7px 7px 7px}
        h2 { margin: 5px; padding: 10px 10px 10px 20px;  background-color:#F0978B;  border-radius: 10px; font-size:120%}
        p { padding-left: 20px; margin-top:5px}
        .genre {margin-top: 20px}
      </style>
		</head>
        <body>         
        <xsl:for-each select="Product">
         <div class="list">
            <h2>
                <xsl:value-of select="Id"/>
                <xsl:text> ... </xsl:text>
                <xsl:value-of select="Name"/>
            </h2>
            <p class="genre">
             <xsl:text> Genre ... </xsl:text>
                <xsl:value-of select="Genre"/>
            </p>
            <p>
             <xsl:text> Description ... </xsl:text>
                <xsl:value-of select="Description"/>
            </p>
            <p> 
             <xsl:text> Number In Stock ... </xsl:text>   
                <xsl:value-of select="NumberInStock"/>
            </p>   
            </div>         
        </xsl:for-each>       
        </body>
		</html>
	</xsl:template>

</xsl:stylesheet>