   M S B A M L   `   `   `  ����A  =Vistas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:VistasVistas  5   	       Vistas.ListaDeEstados   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   $svcclr-namespace:Vistas     5   	     � Title$  ListaDeEstados��5   	   $	��300��6    $	��300��6-    � 	Resources 5         Vistas.ConversorDeEstados   
ConvEstado��%   &	          5   
   6!   5	      .��5
      ��  5   
     Estados � Source � XPath��%O   &	       � 5      $ ./data/Estados.xml?�6.   $ /estados��6J   6]   5      .+�5   
   �� -   5        $��listEstados��5      $��26��6�   $	��120��6�   ��6%   ��  �� Source# [  �� XPath$ 
tipo/@name����6o   ��  �� ElementName$ listEstados�� �� Path$ SelectedItem.Value� �� 	Converter# [  5      �� 5   
   -   5        $��
rectangle1��5   �   $	��100��6~   
���   �$	��200��6�   ��6   �� $ listEstados��$ SelectedItem.Value�# [  6�   5      5      