   M S B A M L   `   `   `  ����E  AClasesBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC0clr-namespace:ClasesBase.ABM;assembly=ClasesBaseClasesBase.ABM  5   	   
 Vistas   Vistas.WinConfig   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @svc0clr-namespace:ClasesBase.ABM;assembly=ClasesBase     5   	     � Title$  Configuracion��5   	   $	��474��6   $	��462��6,    � 
ResizeMode$ NoResize=�68    � Icon$; 5/Vistas;component/Resources/Images/Icons/app-icon.ico��6N   ��5      �� 5   
    �� EndPoint$ 1,0.5&�6    �� 
StartPoint$ 0,0.5&�6/   .��5      �  � Color$ Black��6    � Offset$ 0Q�6)   � 5	      $ 	#FF9AB4D5��6   $ 1Q�6-   � 5
      $ 	#FF3F80F2��6   $ 0.803Q�6-   5      .��5      � .+�5   
   �� -   5        $��tabControl1��5   �   
������� �� TabStripPlacement$
 Left=�6,   $	��365��6E   $
��Left=�6S   $��	12,12,0,0q�6n   $	��Top=�6�   $	��418��6�   .@�5      �� -   5        $��tabItem2��5   &   $��Pieles��6   .��5      � 
�������.+�5      �� -   5        $��
lblEstilos��5   g   $��Elegir: ��6   $��28��6/   $
��Left=�6;   $��6,6,0,0q�6V   $	��Top=�6y   $��55��6�   �� 5      -   5        $��cbSkins��5   Y   $��23��6   $
��Left=�6+   $��	67,11,0,0q�6F    �� SelectedIndex$ 1��6h   $	��Top=�6z   $	��120��6�   6�   5      5      �� 5      -   5        $��tabHistorial��5   +   $��Historiales��6   .��5      �� .@�5      �� $��Login��6   .��5      � 
�������	 � 	Resources	 5         
TABLA_HLOG
 S� 
ObjectType!   ClasesBase.ABM.TrabajarHLog S� 
MethodName��%I   &	        S� 5   "   "
  5   %   $ traerHistorial��5   %   6C   5      .+�5      �� -   5        $��btnExportarHLog��5   �     	btn_style#��[ 6%   $��Exportar��6H   $��23��6[   $
��Left=�6g   $��
88,302,0,0q�6�   $	��Top=�6�   $��95��6�   
 � Tag 5    "   ��5!   &   A=/Vistas;component/Resources/Images/Icons/dark/appbar.save.png��6q   5#      �� 5$      -   5        $��label1��5$   v   $��Exporar log :��6$   $��28��6<   $
��Left=�6H   $��	6,297,0,0q�6c   $	��Top=�6�   �� 5%      -   5        $��stackPanel2��5%   c   $	��254��6)   $
��Left=�66   $��11,6,0,0q�6Q   $	��Top=�6v   $	��320��6�   .+�5&   "   �� -	   5        $��
textBlock4��5&   P     
text_title#��[ 6,   $��Historial de Login��6b   �� 5'   "   -
   5        $��
textBlock5��5'   R     text_descrip#��[ 6,   $C��=Revise la hora y fecha de ingreso al sistema de los usuarios.��6d   �� 5(   "   -   5        $��lwHistorial��5(   K   
�������$	��181��6>   $	��320��6^   ��6j   ��  �� Source# [   �� View 5)   &   �� 5*   *   .P�5+   .   �� $��ID��6f    �� Width$ 25��6r    �� DisplayMemberBinding 6=   �� *	hlog_id+6   �� 5,   .   $��USUARIO��6k   $ 70��6|    6=   �� *usu_username+6�   �� 5-   .   $��FECHA��6i   $ 70��6x    6=   �� *
hlog_fecha+6�   �� 5.   .   $
��HORA��6h   $ 70��6v    6=   �� *	hlog_hora+6�   �� 5/   .   $��DURACION��6l   $ 75��6~    6=   �� *hlog_duracion+6�   50   +   52   #   53      �� 54      -   5        $��
btnLimpiar��54   �   #��[ 6%   $��Limpiar��6H   $��23��6Z   $
��Left=�6f   $��246,302,0,0q�6�   $	��Top=�6�   $��85��6�    55   "   ��56   &   B>/Vistas;component/Resources/Images/Icons/dark/appbar.clean.png��6r   58      59      5:      �� 5;      $��Actividades��6   .��5<      � 
�������	 5=        TABLA_ACTLOG#   ClasesBase.ABM.TrabajarActLog��%K   &	       S� 5>   "   "
  5@   %   $ traerActividades��5A   %   6E   5B      .+�5C      �� -   5        $��stackPanel1��5C   c   $	��277��6)   $
��Left=�66   $��11,6,0,0q�6Q   $	��Top=�6v   $	��320��6�   .+�5D   "   �� -   5        $��
textBlock2��5D   P   #��[ 6,   $	��320��6b   Registro de Actividades5E   :   �� 5F   "   -   5        $��
textBlock3��5F   �   #��[ 6,   $
��Auto��6R   $��11(�6`   $
��Wrap=�6n   $	��320��6�   DBRevise todas las acciones realizadas por los usuarios del sistema.5G   e   �� 5H   "   -   5        $��lwActividades��5H   K   
�������$	��181��6>   $	��320��6`   $��0q�6�   ��6l   �� # [  5I   &   �� 5J   *   .P�5K   .   �� $��ID��6h   $ 25��6t    6=   �� *	actlog_id+6�   �� 5L   .   $��USUARIO��6k   $ 70��6|    6=   �� *usu_username+6�   �� 5M   .   $��DESCRIPCION��6q   $ 70��6�    6=   �� *actlog_descripcion+6�   �� 5N   .   $��FECHA��6k   $ 70��6z    6=   �� *actlog_fecha+6�   �� 5O   .   $
��HORA��6j   $ 75��6x    6=   �� *actlog_hora+6�   5P   +   5R   #   5S      �� 5T      -   5        $��btnExportarAct��5T   �   #��[ 6%   $��Exportar��6I   $��23��6\   $
��Left=�6h   $��
88,302,0,0q�6�   $	��Top=�6�   $��95��6�    5U   "   ��5V   &   A=/Vistas;component/Resources/Images/Icons/dark/appbar.save.png��6q   5X      �� 5Y      -   5        $��label2��5Y   v   $��Exporar log :��6$   $��28��6<   $
��Left=�6H   $��	6,297,0,0q�6c   $	��Top=�6�   �� 5Z      -   5        $��button1��5Z   s   $��Limpiar��6%   $��23��67   $
��Left=�6C   $��246,301,0,0q�6^   #��[ 6�   $	��Top=�6�   $��85��6�    5[   "   ��5\   &   B>/Vistas;component/Resources/Images/Icons/dark/appbar.clean.png��6r   5^      5_      5`      5a      5b      5d      �� 5e   
   -   5        $��
btnAceptar��5e   �   #��[ 6   $��Aceptar��6@   $
��Left=�6R   $��
234,0,0,11q�6m   $��95��6�   $��30��6�   $��Bottom=�6�    5f      ��5g      B>/Vistas;component/Resources/Images/Icons/dark/appbar.check.png��6^   5i      �� 5j   
   -   5        $��btnCancelar��5j   L     btn_style_rojo#��[ 6   $��Cancelar��69   $��30��6_   $��
335,0,0,12q�6k   $��Bottom=�6�   $
��Left=�6�   $��95��6�    5k      ��5l      GC/Vistas;component/Resources/Images/Icons/dark/appbar.arrow.left.png��6c   5n      5o      5p      