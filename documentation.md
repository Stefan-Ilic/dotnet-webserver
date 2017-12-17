---
title: "MyWebServer Dokumentation"
author: "Stefan Ilic"
date: 2017-12-17
titlepage: true
---

Benutzerhandbuch
================

Der Server muss auf einem Rechner gestartet werden, auf dem das .NET Framework bzw. das Mono Framework läuft. Im selben Verzeichnis, in dem das Executable File das Servers läuft, müssen .dll oder .exe Files von Plugins hinzugefügt werden, welche auf Client Requests reagieren. 

Lösungsbeschreibung
===================

Die ersten vier Übungen wurden so gelöst, dass ich nur geschaut haben, dass die Tests passen, ohne überhaupt im Hinterkopf zu haben, dass das mal ein voll funktionsfähiger Webserver sein soll. Erst am Übung fünf, wo man die ersten Plugins implementieren sollte, habe ich gesehen, wie wenig die Unit-Tests eigentlich die Funktionalität des Webservers und er Plugins testen, weshalb ich da zum ersten mal den TcpListener und damit die Server-Funktionalität implementiert habe. Ab da standen die Unit-Tests eher im Hintergrund, ein Task war fertig wenn der Unit-Test ging UND wenn der WebServer mit dem Task ging. Ich nehme an, so soll es auch eigentlich sein.s

Worauf bin ich stolz
====================

Das ist mein erstes größeres Softwareprojekt (das keine Website ist), also habe ich generell darauf geachtet Paradigmen einzuhalten, zum ersten mal in meinem Leben. Resharper war dabei eine große Hilfe, da er ständig nützliche Tipps gibt, wie man seinen Code noch besser machen kann. Außerdem ermahnt er einen sofort, wenn man z.B. Standards in der Namensgebung von Variablen nicht einhält.

Was würde ich das nächste mal anders machen
===========================================

Da ich das TempPlugin als letztes gemacht habe, habe ich leider wenig darauf geachtet dass der Code schön und leserlich ist. Ein Beispiel: Während in vielen anderen Plugins noch einige Linq expressions und schlaue Einzeiler vorkommen, besteht das Temperatur Plugin hauptsächlich aus Schleifen. Ich habe sogar das XML Format für die REST-Schnittstelle selst hard-coded, anstatt von einer C# Klasse generieren zu lassen.

Es war ein kleines Problem, dass 90% des eigentlichen Programms in Übung 6 steckt. Ich habe das glücklicherweise früh genug erkannt und rechtzeitig mit dem eigentlichen Server begonnen, sodass ich im Endeffekt 2 Tage vor Abgabe fertig geworden nicht, und nicht in der Nacht davor. Allerdings sind diese 2 Tage knapp genug, dass ich funktionierende Sachen jetzt noch ungern ändern würde, auch wenn ich eigentlich mit Ihrer Implementierung nicht zu 100% zufrieden bin.